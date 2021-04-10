using System;
using System.Collections.Generic;
using System.Text;

namespace TextRPG2
{
    public enum GameMode
    {
        None,
        Lobby,
        Town,
        Feild
    }
    class Game
    {
        private GameMode mode = GameMode.Lobby;
        private Player player = null;
        private Monster monster = null;
        private Random rand = new Random();

        public void Process()
        {

            switch (mode)
            {
                case GameMode.Lobby:
                    ProcessLobby();
                    break;
                case GameMode.Town:
                    ProcessTown();
                    break;
                case GameMode.Feild:
                    ProcessField();
                    break;
            }
        }
        private void ProcessLobby()
        {
            Console.WriteLine("직업을 선택하세요!\n[1] 기사\n[2] 궁수\n[3] 법사");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    player = new Knight();
                    mode = GameMode.Town;
                    break;
                case "2":
                    player = new Archer();
                    mode = GameMode.Town;
                    break;
                case "3":
                    player = new Mage();
                    mode = GameMode.Town;
                    break;
            }
        }

        private void ProcessTown()
        {
            Console.WriteLine("마을에 입장했습니다!\n[1] 필드로 가기\n[2] 로비로 돌아가기");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    mode = GameMode.Feild;
                    break;
                case "2":
                    mode = GameMode.Lobby;
                    break;
            }
        }
        
        private void ProcessField()
        {
            Console.WriteLine("필드에 입장했습니다!\n[1] 싸우기\n[2] 일정 확률로 마을 돌아가기");
            
            CreateRandomMonster();

            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    ProcessFight();
                    break;
                case "2":
                    TryEscape();
                    break;
            }

        }

        private void TryEscape()
        {
            int RandValue = rand.Next(0, 101);
            if (RandValue < 33)
            {
                mode = GameMode.Town;
            }
            else
            {
                ProcessFight();
            }
        }

        private void ProcessFight()
        {
            while (true)
            {
                int damage = player.GetAttack();
                monster.OnDamaged(damage);
                if (monster.IsDead())
                {
                    Console.WriteLine("승리했습니다!");
                    Console.WriteLine($"남은체력{player.GetHp()}");
                    break;
                }
                damage = monster.GetAttack();
                player.OnDamaged(damage);
                if(player.IsDead())
                {
                    Console.WriteLine("패배했습니다!");
                    mode = GameMode.Lobby;
                    break;
                }

            }
        }

        private void CreateRandomMonster()
        {
            int randValue = rand.Next(0, 3);
            switch (randValue)
            {
                case 0:
                    monster = new Slime();
                    Console.WriteLine("슬라임이 생성되었습니다!");
                    break;
                case 1:
                    monster = new Orc();
                    Console.WriteLine("오크가 생성되었습니다!");
                    break;
                case 2:
                    monster = new Skeleton();
                    Console.WriteLine("해골이 생성되었습니다!");
                    break;

            }
        }
    
    }
    
}
