﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TTTProject.Models;

namespace TTTProject.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult FinishGame()
    {
        return View();
    }

    [HttpPost]
    public IActionResult StartGame(string firstPlayerName, string secondPlayerName)
        => View("Game", new GameDataModel(firstPlayerName, secondPlayerName));

    [HttpPost]
    public IActionResult Game(int idPole, string fieldString, bool isX, string firstPlayerName, string secondPlayerName)
    {
        var gameData = new GameDataModel(firstPlayerName, secondPlayerName, fieldString, isX);
        gameData.MakeAMove(idPole);
        return View(gameData);
    }

    public IActionResult Privacy()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
