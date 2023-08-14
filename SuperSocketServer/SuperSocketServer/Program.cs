using SuperSocketServer;

var server = new MainServer();
server.InitConfig();
server.CreateServer();
var IsResult = server.Start();

if (IsResult)
{
    Console.WriteLine("서버 네트워크 시작");
}
else
{
    Console.WriteLine("[ERROR] 서버 네트워크 시작 실패");
    return;
}

while (Console.ReadKey().KeyChar != 'q')
{
    Console.WriteLine();
    continue;
}

server.Stop();


