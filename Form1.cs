using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zad4_game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();
        }

        Random random = new Random();
        //
        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k","k",
            "b","b","v","v","w","w","z","z"
        };

        // firstClicked указывает на первый элемент управления  
        // который щелкает игрок, но он будет нулевым 
        // если игрок еще не нажал на ярлык 
        Label firstClicked = null;

        // второй щелчок указывает на второй элемент управления меткой
        // на который нажимает игрок
        Label secondClicked = null;

        private void AssignIconsToSquares()
        {
            // Панель TableLayoutPanel содержит 16 меток,
            // и список значков содержит 16 значков,
            // таким образом, значок выбирается случайным образом из списка
            // и добавляется к каждой метке
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    // значок-метка.ForeColor = метка значка.Задний цвет;
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                    
                }
            }
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            // Таймер включается только после того, как игрокам
            // были показаны несовпадающих знака
            // поэтому игнорируйте любые щелчки, если таймер работает
            if (timer1.Enabled == true)
                return;
            Label clickedLabel = sender as Label;
            if (clickedLabel != null)
            {
                // Если выбранная метка черная, игрок нажал
                // значок, который уже был показан 
                // игнорировать щелчок
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                //clickedLabel.ForeColor = Color.Black;

                //  Если значение firstClicked равно null, это первый значок 
                // в паре, на который нажал игрок,
                // поэтому установите значение firstClicked на метку, которую игрок
                // щелкнул, изменил его цвет на черный и верните
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;

                    return;
                }
                // Если игрок заходит так далеко, таймер не
                // сработает, а значение firstClicked не равно null,
                // так что это, должно быть, вторая иконка, на которую нажал игрок
                // Установите его цвет на черный
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;
                // Проверьте, выиграл ли игрок
                CheckForWinner();
    

                // Если игрок нажал на две одинаковые иконки, сохраните их
                // черный и сброс при первом нажатии и при втором нажатии
                // чтобы игрок мог нажать на другой значок
                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                // Если игрок зайдет так далеко, то игрок
                // щелкнул по двум разным значкам, поэтому запустите
                // таймер (который будет ждать три четверти
                // секунду, а затем скройте значки)
                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Остановка таймера
            timer1.Stop();

            // Скрыть оба значка
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            // Сброс первого нажатия и второго нажатия 
            // итак, в следующий раз, когда метка будет
            // щелкнул, программа знает, что это первый щелчок
            firstClicked = null;
            secondClicked = null;
         }

        private void CheckForWinner()
        {
            // Просмотрите все метки в TableLayoutPanel,
            // проверяя каждую из них, чтобы увидеть, соответствует ли ее значок
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }

                //Если цикл не вернулся, значит, 
                //пользователь выиграл. сообщение и закрие формы
                MessageBox.Show("Вы подобрали все значки!", "Поздравляю");
                Close();
            }
        }
    }

}
//жесть