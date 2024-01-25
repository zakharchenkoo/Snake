using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    static class Program
    {
        // Вхідна точка програми
        [STAThread]
        static void Main()
        {
            // Включення візуальних стилів програми
            Application.EnableVisualStyles();

            // Встановлення сумісності з рендерером тексту програми
            Application.SetCompatibleTextRenderingDefault(false);

            // Запуск головної форми
            Application.Run(new frmGame());
        }
    }
}
