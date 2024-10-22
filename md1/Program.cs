string path = "./data.json";
var dm = new DataManager();

dm.CreateTestData();
Console.WriteLine(dm.Print());

dm.Save(path);

dm.Reset();
Console.WriteLine(dm.Print());

dm.Load(path);
Console.WriteLine(dm.Print());

Console.ReadLine();
