using TTTProject.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TTTProject.HtmlHelpers;

public static class GamePageHtmlHelpers
{
    //возвращает имя игрока, чья очередь играть, на основе свойства «IsX» объекта «GameDataModel»
    public static string ShowPlayerName(this IHtmlHelper helpers, GameDataModel dataModel) =>
        dataModel.IsX ? dataModel.FirstPlayer.Name : dataModel.SecondPlayer.Name;

    // отображает игровую ячейку с помощью кнопки,
    // если ячейка пустая и победитель еще не определен,
    // или отображает значение ячейки, если она уже заполнена.
    public static HtmlString ShowCell(this IHtmlHelper helpers, GameDataModel dataModel, int currentId) =>
        new HtmlString(dataModel.Field[currentId] == "" && dataModel.Winner == -1
            ? $"<button class='Cells' name='idPole' value='{currentId}' formaction='/Home/Game'></button>"
            : dataModel.Field[currentId]);
    //создает скрытые поля формы, которые содержат данные модели игры.
    public static HtmlString DataModel(this IHtmlHelper helpers, GameDataModel dataModel) =>
        new HtmlString($@"<input type='hidden' name='IsX' value='{dataModel.IsX}'/>
        <input type='hidden' name='FieldString' value='{dataModel.FieldString}'/>
        <input type='hidden' name='firstPlayerName' value='{dataModel.FirstPlayer.Name}'/>
        <input type='hidden' name='secondPlayerName' value='{dataModel.SecondPlayer.Name}'/>");
}