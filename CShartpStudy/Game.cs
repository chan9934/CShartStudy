﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CShartpStudy
{
    enum GameMode
    {
        None,
        Lobby,
        Town,
        Field
    }
    class Game
    {
        private GameMode mode = GameMode.Lobby;
        private Player player;
        private Monster monster;
        private Random random = new Random();

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
                case GameMode.Field:
                    ProcessField();
                    break;
            }
        }

        private void ProcessField()
        {
            Console.WriteLine("필드에 입장했습니다.");
            CreateRandomMonster();
            Console.WriteLine("[1] 싸우기");
            Console.WriteLine("[2] 도망치기");

            string Index = Console.ReadLine();

            switch (Index)
            {
                case "1":
                    Battle();
                    break;
                case "2":
                    Gacha();
                    break;
            }
        }
        private void Gacha()
        {
            int rando = random.Next(0, 101);
            if (rando < 33)
            {
                Console.WriteLine("도망치는데 성공했습니다");
                mode = GameMode.Town;
            }
            else
            {
                Console.WriteLine("도망치는데 실패했습니다");
                Battle();
            }
        }

        private void Battle()
        {
            while (true)
            {
                int Damage = player.GetAttack();
                monster.OnDamaged(Damage);
                if (monster.IsDead())
                {
                    Console.WriteLine("승리했습니다");
                    Console.WriteLine($"남은 체력 : {player.GetHp()}");
                    break;
                }

                Damage = monster.GetAttack();
                player.OnDamaged(Damage);
                if (player.IsDead())
                {
                    Console.WriteLine("패배했습니다");
                    mode = GameMode.Lobby;
                    break;
                }
            }
        }
        private void CreateRandomMonster()
        {
            int randValue = random.Next(0, 3);
            switch (randValue)
            {
                case 0:
                    monster = new Slime();
                    Console.WriteLine("슬라임이 나타났다");
                    break;
                case 1:
                    monster = new Orc();
                    Console.WriteLine("오크가 나타났다");
                    break;
                case 2:
                    monster = new Skeleton();
                    Console.WriteLine("해골이 나타났다");
                    break;
            }
        }
        private void ProcessLobby()
        {
            Console.WriteLine("직업을 선택하세요!");
            Console.WriteLine("[1] 기사 [2] 궁수 [3] 법사");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    player = new Knight();
                    break;
                case "2":
                    player = new Archer();
                    break;
                case "3":
                    player = new Mage();
                    break;
            }

            if (player != null)
            {
                mode = GameMode.Town;
            }
        }

        private void ProcessTown()
        {
            Console.WriteLine("마을에 입장햇습니다!");
            Console.WriteLine("[1]필드로 가기");
            Console.WriteLine("[2]로비로 돌아가기");

            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    mode = GameMode.Field;
                    break;
                case "2":
                    mode = GameMode.Lobby;
                    break;
            }


        }
    }
}