using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketBase.Logging;
using SuperSocket.SocketBase.Protocol;


namespace SuperSocketServer
{

    class MainServer : AppServer<NetworkSession, EFBinaryRequestInfo>
    {
        Dictionary<int, Action<NetworkSession, EFBinaryRequestInfo>> HandlerMap = new Dictionary<int, Action<NetworkSession, EFBinaryRequestInfo>>();
        CommonHandler CommonHan = new CommonHandler();

        IServerConfig m_Config;

        public MainServer() 
            : base(new DefaultReceiveFilterFactory<ReceiveFilter, EFBinaryRequestInfo>())
        {
            NewSessionConnected += new SessionHandler<NetworkSession>(OnConnected);
            SessionClosed += new SessionHandler<NetworkSession, CloseReason>(OnClosed);
            NewRequestReceived += new RequestHandler<NetworkSession, EFBinaryRequestInfo>(RequestReceived);
        }

        void RegistHandler()
        {
            HandlerMap.Add((int)PACKETID.REQ_ECHO, CommonHan.RequestEcho);

            Console.WriteLine("핸들러 등록 완료");
        }

        public void InitConfig()
        {
            m_Config = new ServerConfig
            {
                Port = 18732,
                Ip = "Any",
                MaxConnectionNumber = 100,
                Mode = SocketMode.Tcp,
                Name = "BoardServerNet"
            };
        }

        public void CreateServer()
        {
            bool bResult = Setup(new RootConfig(), m_Config, logFactory: new Log4NetLogFactory());

            if(bResult == false)
            {
                Console.WriteLine($"[ERROR] 서버 네트워크 설정 실패");
                return;
            }

            RegistHandler();

            Console.WriteLine("서버 생성 성공");
        }

        public bool IsRunning(ServerState eCurState)
        {
            if (eCurState == ServerState.Running)
            {
                return true;
            }

            return false;
        }

        void OnConnected(NetworkSession session)
        {
            Console.WriteLine($"세션 번호 {session.SessionID} 접속");
        }

        void OnClosed(NetworkSession session, CloseReason reason)
        {
            Console.WriteLine($"세션 번호 {session.SessionID} 접속 해제: {reason}");
        }

        void RequestReceived(NetworkSession session, EFBinaryRequestInfo reqInfo)
        {
            Console.WriteLine($"세션 번호 {session.SessionID} 받은 데이터 크기: {reqInfo.Body.Length}");
        }
    }


    public class NetworkSession : AppSession<NetworkSession, EFBinaryRequestInfo>
    {

    }
}
