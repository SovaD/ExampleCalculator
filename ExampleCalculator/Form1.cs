using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExampleCalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string input = "";
        string exp = "";
        bool isLight = false;
        private void button_Click(object sender, EventArgs e)
        {//обработка нажатия на кнопки
            Button button = (Button)sender;
            //sign - назначение кнопки (цифры, операторы)
            string sign = button.Name.Substring("button".Length);

            if ("0123456789".Contains(sign))
            {//обработка ввода числа
                input = input + sign;
                //inputNum=double.Parse(input);
                labelResult.Text = input;
            }
            else if (sign == "AC")
            {
                //all clear
                labelResult.Text = "0";
                input = "";
            }
            else if (sign == "C")
            {
                //clearing from the end
                if (input.Length >= 1)
                    input = input.Substring(0, input.Length - 1);

                labelResult.Text = input == "" ? "0" : input;
            }
            else if (sign.ToLower() == "div")
            {
                //обработка деления /
                exp = labelResult.Text + "/";
                input = "";
            }
            else if (sign.ToLower() == "mul")
            {
                //обработка умножения *
                exp = labelResult.Text + "*";
                input = "";
            }
            else if (sign.ToLower() == "sub")
            {
                //обработка вычитания -
                exp = labelResult.Text + "-";
                input = "";
            }
            else if (sign.ToLower() == "add")
            {
                //обработка суммирования +
                exp = labelResult.Text + "+";
                input = "";
            }
            else if (sign.ToLower() == "equ")
            {
                //обработка знака =

                string res = equ();
                if (res == "err0")
                    labelResult.Text = "error div by 0";
                labelResult.Text = res;
                exp += input;
                input = "";
            }
            else if (sign.ToLower() == "percent")
            {
                //обработка знака %
                string res = equ(true);
                if (res == "err0")
                    labelResult.Text = "error div by 0";
                labelResult.Text = res;
                exp += input + "%";
                input = "";

            }
            else if (sign.ToLower() == "point")
            {
                //обработка точки .
                input = checkPoint();
                labelResult.Text = input;
            }
            else if (sign.ToLower() == "mode")
            {
                //обработка переключения режима (темная/светлая)
                changeMode();
            }

            labelExpression.Text = exp;
        }
        string checkPoint()
        {
            //обработка десятичной записи
            if (input.Contains(",")) return input;
            if (input == "") input = "0";
            try
            {
                input += ",";
                double num = double.Parse(input);
            }
            catch (Exception ex) { return input; }
            return input;
        }
        string equ(bool isPercent = false)
        {
            try
            {
                //оператор
                char sign = exp.Last();
                //операнд 1
                double a = double.Parse(exp.Substring(0, exp.Length - 1));
                //операнд 2
                double b = double.Parse(input);
                if (isPercent) b = a * b / 100;
                double result = 0;
              
                switch (sign)
                {
                    case '/':
                        if (b == 0) return "err0";
                        result = a / b; break;
                    case '*': result = a * b; break;
                    case '-': result = a - b; break;
                    case '+': result = a + b; break;
                }
                return result.ToString();
            }
            catch (Exception ex) { return input; }
        }
        void changeMode()
        {
            if (isLight)
            {
                this.BackColor = Color.DimGray;
                this.ForeColor = Color.WhiteSmoke;
                labelResult.BackColor = Color.DimGray;
                labelResult.ForeColor = Color.WhiteSmoke;
                buttonsDarkStyle();
            }
            else
            {
                this.BackColor = Color.White;
                this.ForeColor = Color.Black;
                labelResult.BackColor = Color.White;
                labelResult.ForeColor = Color.Black;
                buttonsLightStyle();
            }
            isLight = isLight == true ? false : true;
        }
        void buttonsDarkStyle()
        {
            //перебираем все кнопки в таблице и задаем им свойства
            List<Button> list = tableLayoutPanel1.Controls.OfType<Button>().ToList();
            foreach (Button button in list)
            {
                    button.BackColor = Color.DimGray;
                    button.ForeColor = Color.DarkGray;
                    button.FlatAppearance.BorderColor = Color.DarkGray;
                
            }
        }
        void buttonsLightStyle()
        {  //перебираем все кнопки в таблице и задаем им свойства
            List<Button> list = tableLayoutPanel1.Controls.OfType<Button>().ToList();
            foreach (Button button in list)
            {

                button.BackColor = Color.White;
                button.ForeColor = Color.DimGray;
                button.FlatAppearance.BorderColor = Color.DimGray;

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            changeMode();
        }

    }

}
