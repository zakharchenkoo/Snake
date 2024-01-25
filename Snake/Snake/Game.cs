using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class frmGame : Form
    {
        // оголошуємо змінні для представлення змійки та їжі
        private List<Circle> snake = new List<Circle>();
        private Circle food = new Circle();

        public frmGame()
        {
            InitializeComponent();
        }

        private void startGame()
        {
            // Підготовка до початку гри
            lblGameOver.Visible = false;
            btnPlay.Enabled = false;

            new Settings();
            
            tmrTimer.Interval = 1000 / Settings.speed;
            tmrTimer.Tick += updateScreen;
            tmrTimer.Start();

            // Очистити змійку
            snake.Clear();

            // Створити голову змійки
            Circle snakeHead = new Circle() { x = 16, y = 10 };
            snake.Add(snakeHead);

            // Встановити початковий рахунок та створити їжу
            lblScore.Text = Settings.score.ToString();
            generateFood();
        }

       
        private void generateFood()
        {
            // Генерація нової їжі
            int maxXPos = (picCanvas.Size.Width / Settings.width);
            int maxYPos = (picCanvas.Size.Height / Settings.height);

            Random random = new Random();
            food = new Circle() { x = random.Next(0, maxXPos), y = random.Next(0, maxYPos) };
        }

        private void updateScreen(object sender, EventArgs e)
        {
            // Оновлення стану екрану
            // Якщо гра закінчилась
            if (Settings.gameOver)
            {

                if (Input.KeyPressed(Keys.Return)) { startGame(); }
            }
            else
            {
                // Якщо гра триває
                Settings.duration += 1;

                // Зміна напрямку руху змійки залежно від натискання клавіш
                if (Input.KeyPressed(Keys.D) && Settings.direction != Direction.Left)
                    Settings.direction = Direction.Right;
                else if (Input.KeyPressed(Keys.A) && Settings.direction != Direction.Right)
                    Settings.direction = Direction.Left;
                else if (Input.KeyPressed(Keys.W) && Settings.direction != Direction.Down)
                    Settings.direction = Direction.Up;
                else if (Input.KeyPressed(Keys.S) && Settings.direction != Direction.Up)
                    Settings.direction = Direction.Down;

                // Рух змійки та оновлення рахунку та таймера
                movePlayer();
                lblTimer.Text = TimeSpan.FromSeconds(Settings.duration / Settings.speed).ToString();
                lblScore.Text = Settings.score.ToString();

            }
                // Оновлення полотна гри
               picCanvas.Refresh();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Вихід з програми
            Application.Exit();
        }

        private void picCanvas_Paint(object sender, PaintEventArgs e)
        {
            // Малювання гри
            Graphics canvas = e.Graphics;

            if (Settings.gameOver)
            {
                // Якщо гра закінчилась
                string msg = "Game Over!\nКількість набраних балів: " + Settings.score;
                lblGameOver.Text = msg;
                lblGameOver.Visible = true;
            }
            else
            {
                // Якщо гра триває
                Brush snakeColour;
               
                for (int i=0; i<snake.Count; ++i)
                {
                    // Малюємо змійку
                    if (i == 0)
                        snakeColour = Brushes.Black;
                    
                    else
                        snakeColour = Brushes.Green;
                   
                    canvas.FillEllipse(snakeColour,
                        new Rectangle(snake[i].x * Settings.width,
                        snake[i].y * Settings.height,
                        Settings.width, Settings.height));

                    //Малюємо їжу
                    canvas.FillEllipse(Brushes.Red,
                        new Rectangle(food.x * Settings.width,
                        food.y * Settings.height, Settings.width,
                        Settings.height));

                }

            }

        }

        private void movePlayer()
        {
            // Рух змійки
            for (int i=snake.Count-1; i>=0; i--) {
               
                if (i == 0) {
                    // Рух голови змійки залежно від напрямку
                    switch (Settings.direction) {
                        case Direction.Right:
                            snake[i].x++;
                            break;
                        case Direction.Left:
                            snake[i].x--;
                            break;
                        case Direction.Up:
                            snake[i].y--;
                            break;
                        case Direction.Down:
                            snake[i].y++;
                            break;
                    }

                    // Перевірка на зіткнення з краєм поля або самою змійкою
                    int maxXPos = (picCanvas.Size.Width / Settings.width);
                    int maxYPos = (picCanvas.Size.Height / Settings.height);
                    
                    if (snake[i].x < 0 || snake[i].y < 0 
                        || snake[i].x >= maxXPos || snake[i].y >= maxYPos) {
                        die();
                    }
                    
                    for (int j=1; j<snake.Count; ++j) {
                        if (snake[i].x == snake[j].x && snake[i].y == snake[j].y) {
                            die();
                        }
                    }

                    // Перевірка на поїдання їжі
                    if (snake[i].x == food.x && snake[i].y == food.y) {
                        eat();
                    }
                }
                
                else {
                    // Рух решти тіла змійки
                    snake[i].x = snake[i - 1].x;
                    snake[i].y = snake[i - 1].y;
                }

            }
        }

        private void eat()
        {
            // Зміна розміру змійки та оновлення рахунку
            Circle food = new Circle();
            food.x = snake[snake.Count - 1].x;
            food.y = snake[snake.Count - 1].y;
            snake.Add(food);
           
            Settings.score += Settings.points;
           
            generateFood();

            // Змінюємо швидкість, якщо кількість балів змінюється на 10
            if (Settings.score % 10 == 0 && Settings.score != 0)
            {
                Settings.speed += 2;
                tmrTimer.Interval = 1000 / Settings.speed; 
            }
        }

        private void die()
        {
            // Кінець гри
            Settings.gameOver = true;
            tmrTimer.Tick -= updateScreen;
            tmrTimer.Stop();
            btnPlay.Enabled = true;
        }

        private void frmGame_KeyDown(object sender, KeyEventArgs e)
        {
            // Обробник натискання клавіші
            Input.ChangeState(e.KeyCode, true);
        }

        private void frmGame_KeyUp(object sender, KeyEventArgs e)
        {
            // Обробник відпускання клавіші
            Input.ChangeState(e.KeyCode, false);
        }
       
        private void btnPlay_Click(object sender, EventArgs e)
        {
            // Обробник кнопки Play
            Focus();
            startGame();
        }

        private void frmGame_Load(object sender, EventArgs e)
        {
            // Обробник події завантаження форми
            btnPlay.Text = "Play";
            tmrTimer.Enabled = false;
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            // Обробник кнопки Exit
            Application.Exit();
        }

    }
}
