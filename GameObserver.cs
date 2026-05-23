using System;

namespace GameObserver
{
    class Player
    {
        public int HP { get; private set; }

        public event Action<int> OnDamageTaken;

        public Player(string name, int hp)
        {
            Name = name;
            HP = hp;
        }

        public void TakeDamage(int damage)
        {
            HP -= damage;

            Console.WriteLine($"\n{Name} отримав {damage} урону");

            OnDamageTaken?.Invoke(HP);
        }
    }

    class UIHealthBar
    {
        public void UpdateHP(int hp)
        {
            Console.WriteLine($"[UI] Поточне HP: {hp}");
        }
    }

    class SoundSystem
    {
        public void PlaySounds(int hp)
        {
            Console.WriteLine("[Sound] Відтворення звуку отримання урону");

            if (hp <= 20)
            {
                Console.WriteLine("[Sound] Критичний стан!");
            }
        }
    }

    class AchievementSystem
    {
        private bool halfHealthUnlocked = false;
        private bool firstDeathUnlocked = false;

        public void CheckAchievements(int hp)
        {
            if (hp <= 50 && !halfHealthUnlocked)
            {
                Console.WriteLine("[Achievement] Half Health");
                halfHealthUnlocked = true;
            }

            if (hp <= 0 && !firstDeathUnlocked)
            {
                Console.WriteLine("[Achievement] First Death");
                firstDeathUnlocked = true;
            }
        }
    }

    class GameLogger
    {
        public void Log(int hp)
        {
            Console.WriteLine($"[Logger] Поточне HP після урону: {hp}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player(100);

            UIHealthBar ui = new UIHealthBar();
            SoundSystem sound = new SoundSystem();
            AchievementSystem achievements = new AchievementSystem();
            GameLogger logger = new GameLogger();

            player.OnDamageTaken += ui.UpdateHP;
            player.OnDamageTaken += sound.PlaySounds;
            player.OnDamageTaken += achievements.CheckAchievements;
            player.OnDamageTaken += logger.Log;

            player.TakeDamage(20);
            player.TakeDamage(35);
            player.TakeDamage(30);
            player.TakeDamage(20);
        }
    }
}