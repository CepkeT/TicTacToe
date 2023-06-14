namespace TTTProject.Models;

public class GameDataModel
{
    public PlayerDataModel FirstPlayer { get; set; }

    public PlayerDataModel SecondPlayer { get; set; }

    public string[] Field { private set; get; }

    public string FieldString { set; get; }

    //значение, указывающее, играет ли текущий игрок как X или O.
    public bool IsX { get; set; }

    //значение, указывающее, продолжается ли игра
    public bool IsMove { get; set; }

    //значение, представляющее победителя игры (1 — X, 2 — O и 0 — ничья).
    public int Winner { get; private set; }

    public GameDataModel(string firstPlayer, string secondPlayer, string fieldString, bool isX)
    {  
        FirstPlayer = new PlayerDataModel(firstPlayer);
        SecondPlayer = new PlayerDataModel(secondPlayer);
        FieldString = fieldString;
        IsX = isX;
        Field = FieldString.Split(',');
        IsMove = !(FieldString.Length == 17);
        Winner = -1;
    }

    public GameDataModel(string firstPlayer, string secondPlayer)
    {
        FirstPlayer = new PlayerDataModel(firstPlayer);
        SecondPlayer = new PlayerDataModel(secondPlayer);
        IsX = true;
        FieldString = string.Empty;
        Field = new string[9];
        IsMove = true;
        FillTheField();
        Winner = -1;
        DeterminingWinner();
    }

    public GameDataModel()
    {
        FirstPlayer = new PlayerDataModel();
        SecondPlayer = new PlayerDataModel();
        IsX = true;
        FieldString = string.Empty;
        Field = new string[9];
        IsMove = true;
    }

    private void FillTheField()
    {
        //заполнение поля 
        var random = new Random();
        for (var i = 0; i < 9; i++)
            Field[i] = (random.Next(0, 3) switch { 0 => "", 1 => "X", 2 => "O" });

        FieldString = string.Join(",", Field);
        IsMove = FieldString.Length != 17;
        DeterminingWinner();
    }

    private void DeterminingWinner()
    {
        //проверяет все возможные выигрышные комбинации на игровом поле и устанавливает 
        //кто именно победил
        var combinations = new[]
        {
            new[] { 0, 1, 2 }, new[] { 3, 4, 5 }, new[] { 6, 7, 8 }, new[] { 0, 3, 6 }, new[] { 1, 4, 7 },
            new[] { 2, 5, 8 }, new[] { 0, 4, 8 }, new[] { 2, 4, 6 }
        };
        foreach (var t in combinations)
            if (Field[t[0]] == Field[t[1]] &&
                Field[t[0]] == Field[t[2]] && Field[t[0]] != "")
            {
                IsMove = false;
                Winner = Field[t[0]] == "X" ? 1 : 2;
                return;
            }

        if (!IsMove && Winner == -1) Winner = 0;
    }

    public void MakeAMove(int id)
    {
        //устанавливает значение  по
        //указанному индексу равным X или O в зависимости от значения свойства `IsX`
        //переключает значение свойства `IsX`
        //проверяет определен ли победитель
        Field[id] = IsX ? "X" : "O";
        IsX = !IsX;
        FieldString = string.Join(",", Field);
        IsMove = !(FieldString.Length == 17);
        DeterminingWinner();
    }
}