using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Cat_project
{
    public partial class Form1 : Form
    {
        
        int i = 0;
        public Form1()
        {
            InitializeComponent();
            label1.Font = new Font("Tobota", 14, FontStyle.Italic);
            label2.Font = new Font("Tobota", 12, FontStyle.Italic);
            label3.Font = new Font("Tobota", 12, FontStyle.Italic);
            label4.Font = new Font("Tobota", 12, FontStyle.Italic);
            label1.Text = "Времена нынче тяжелые\n хозяйка заставила оцифроваться\n и на корм зарабатывать \n хотите погадаю?  Если у Вас\n сломался компьютер,\n могу подсказать что с ним";
           

            pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\cart\\b_06.png");
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            Timer timer1 = new Timer();
            timer1.Enabled = true;
            timer1.Interval = 5000;
            timer1.Tick += new EventHandler(timer1_Tick);

            var nums = Enumerable.Range(1, 36).ToList();
            Karta[] karts = new Karta[16];
            //int[] result = new int[16];
            Random rand = new Random();
            for (int i = 0; i < 16; i++)
            {
                int pos = rand.Next(0, nums.Count);
                karts[i] = new Karta
                {
                    Id = nums[pos],
                    Link = BDcom.GetSqlString("SELECT [link] FROM [dbo].[Card] where [id] =" + nums[pos].ToString()),
                    Text = BDcom.GetSqlString("SELECT [text] FROM [dbo].[Card] where [id] =" + nums[pos].ToString()),
                    Sign = Convert.ToInt32(BDcom.GetSqlString("SELECT [etc] FROM [dbo].[Card] where [id] =" + nums[pos].ToString()))
                };
                (Controls["pictureBox" + (i+1).ToString()] as PictureBox).Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\cart\\" + karts[i].Link);
                (Controls["pictureBox" + (i+1).ToString()] as PictureBox).SizeMode = PictureBoxSizeMode.StretchImage;
                // karts[i] = Karta(nums[pos], BDcom.GetSqlString("SELECT [link] FROM [dbo].[Card] where [id] =" + karts[i - 1].id.ToString());
                //(nums[pos],);
                nums.RemoveAt(pos);
            }
            foreach (var x in karts)
            {
                textBox1.Text+= " "+ x.Sign + Environment.NewLine;
            }
            for (int i = 0; i < 16; i++)
            {
                if (((i+1)%2==0) && (i!=0)) {
                    if (((karts[i].Sign == 1) && (1 < karts[i - 1].Sign && 12 > karts[i - 1].Sign)) ||
                        ((karts[i].Sign == 2) && (karts[i - 1].Sign > 2 && karts[i - 1].Sign < 12)) ||
                        ((karts[i].Sign == 3) && ((karts[i - 1].Sign == 7) || (karts[i - 1].Sign == 8))) ||
                        ((karts[i].Sign == 4) && ((karts[i - 1].Sign == 7) || (karts[i - 1].Sign == 8))) ||
                        ((karts[i].Sign == 5) && ((karts[i - 1].Sign == 4) || (karts[i - 1].Sign > 5 && karts[i - 1].Sign < 11))))
                    {
                        (karts[i - 1], karts[i]) = (karts[i], karts[i - 1]);
                    }
                    if (((karts[i - 1].Sign == 1) && (1 < karts[i].Sign && 12 > karts[i].Sign)) ||
                       ((karts[i - 1].Sign == 5) && ((karts[i].Sign == 4) || (karts[i].Sign == 10))))
                    {
                        karts[i - 1].Text += " на";
                        karts[i].Text = BDcom.GetSqlString("SELECT [na] FROM [dbo].[Card] where [id] =" + karts[i].Id.ToString());
                    }
                    if (((karts[i - 1].Sign == 2) && (karts[i].Sign > 2 && karts[i].Sign < 12)) ||
                       ((karts[i - 1].Sign == 5) && (karts[i].Sign > 5 && karts[i].Sign < 9)))
                    {
                        karts[i - 1].Text += " к";
                        karts[i].Text = BDcom.GetSqlString("SELECT [k] FROM [dbo].[Card] where [id] =" + karts[i].Id.ToString());
                    }
                    if (((karts[i - 1].Sign == 3) || (karts[i - 1].Sign == 4)) && ((karts[i].Sign == 7) || (karts[i].Sign == 8)))
                    {
                        karts[i - 1].Text += " c";
                        karts[i].Text = BDcom.GetSqlString("SELECT [s] FROM [dbo].[Card] where [id] =" + karts[i].Id.ToString());
                    }
                   // if ((karts[i - 1].Sign == 5) && (karts[i].Sign > 5 && karts[i].Sign < 9))
                   // {
                   //     karts[i - 1].Text += " к ";
                   // }
                  //  if ((karts[i - 1].Sign == 5) && ((karts[i].Sign == 4) || (karts[i].Sign == 10)))
                //    {
                 //       karts[i - 1].Text += " на ";
                  //  }
                }
            }
            textBox1.Text += Environment.NewLine;
            for (int i = 0; i<16; i++)
            {
                //    textBox1.Text += " " + x.Text + Environment.NewLine;
                if (i < 6)
                {
                    label4.Text +=  karts[i].Text + " ";
                    if (i == 3)
                        label4.Text += Environment.NewLine;
                }
                else
                    if (i < 10)
                {
                    label3.Text += karts[i].Text + " ";
                    if (i == 7)
                        label3.Text += Environment.NewLine;
                }
                else

                {
                    label2.Text += karts[i].Text + " ";
                    if (i == 13)
                        label2.Text += Environment.NewLine;
                }
                // label4.Text += "";
            }

            // foreach (Control x in this.Controls)
            //  {
            //    if (x is PictureBox)
            // for (int i = 1; i < 17; i++)
            //  {
            //    (Controls["pictureBox" + i.ToString()] as PictureBox).Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\cart\\" + BDcom.GetSqlString("SELECT [link] FROM [dbo].[Card] where [id] =" + karts[i - 1].Id.ToString()));
            //      (Controls["pictureBox" + i.ToString()] as PictureBox).SizeMode = PictureBoxSizeMode.StretchImage;
            //   }


            //for (int i = 1; i < 17; i++)
            //    {
            //   (Controls["pictureBox" + i.ToString()] as PictureBox).Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\cart\\" + BDcom.GetSqlString("SELECT [link] FROM [dbo].[Card] where [id] =" + result[i - 1].ToString()));
            //}
            //for (int i = 7; i < 12; i++)
            //(Controls["pictureBox" + i.ToString()] as PictureBox).Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\cart\\" + BDcom.GetSqlString("SELECT [link] FROM [dbo].[Card] where [id] =" + result[i].ToString()));
            //TextBox[] textBoxes = this.Controls.OfType<TextBox>().ToArray();
            // pictureBoxes = this.Controls.OfType<PictureBox>().ToArray();
            //for (int i=0; i<pictureBoxes.Length;i++)
            //  {
            // pictureBoxes[i].Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\cart\\b_0" + result[i].ToString() + ".png");

            //  var t = "PictureBox" + i;
            //  var textbox = this.Controls[t];
            //textbox.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\cart\\b_0" + result[i].ToString() + ".png");
            //    }  // = Image.FromFile(Directory.GetCurrentDirectory() + "\\cart\\b_06" + ".png");
            //((PictureBox)x).Image == Image.FromFile(Directory.GetCurrentDirectory() + "\\cart\\b_0" + result[i].ToString() + ".png");
            //   }
            //  }


            //  for (var i = 0; i < 5; i++)
            //((PictureBox)i).Image == Image.FromFile(Directory.GetCurrentDirectory() + "\\cart\\b_0" + result[i].ToString() + ".png");
            //PictureBox[] pictureBoxes = this.Controls.OfType<PictureBox>().ToArray();
            //for (int i = 1; i < 7; i++)
            //{
            // .Where(x => x.Name.EndsWith("1")  = Image.FromFile(Directory.GetCurrentDirectory() + "\\cart\\b_0" + result[i].ToString() + ".png");
            //}
        }


        private void Form1_Load_1(object sender, EventArgs e)
        {
            dataGridView1.DataSource = BDcom.GetSqlTable("SELECT * FROM [dbo].[Card]");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (i != 17)
            {
                label1.Text = mas[i];
                i++;
            }
            else
                i = 0;
        }
        string[] mas = new string[] { "Гадаю я так себе,\n потому что у меня лапки" ,
            "Просил открыть дверь,\n мне открыли, а это не та\n дверь, пошёл обратно...",
            "Гадаю я так себе, потому что у меня лапки",
            "Преставляете хотел\n на солнышке погреться, \n а мне открыли\n не тот балкон...",
            "Вот уже год питаюсь\n одним кормом и водой, \n а Вы так сможете?",
            "Недавно узнал, что\n красной точкой управляют...",
            "На краю стола стоит\n кружка с чаем, так бесяче, \n  пойду её подовину...",
            "Пойду выражу своё мнение\n об этой программе\n  в лоток...",
            "Корм опять пропал\n из миски...",
            "Не разрешают драть\n мои обои...",
            "Что за люди?! Снова\n на ночь заняли мою кровать...",
            "Жрать или не жрать,\n вот в чём вопрос...",
            "Вот народ, сами кормят\n и сами жирным называют\n а я не жирный, это у меня кость пушистая",
            "Хозяйка отошла и накрыла\n свою тарелку \n с едой плёнкой не хороший всё \n же она человечек...",
            "Все хотят видеть котика,\n но ни кто не \n спрашивает, хочет ли котик \n видеть всех ",
            "В осенние холодные деньки,\n всё чаще думаю,\n как хорошо быть толстеньким \n ленивым котиком",
            "Утром люди выходят,\n а вечером заходят...\n Интересно, что они весь день делают \n за дверью",
            "По скорее бы новый год,\n огоньки  будут мигать\n вокруг атмосфера чуда и волшебства...\n Ёлку уроню ",
            "Сидел я сегодня на\n подоконнике, как обычно \n и цветок ни с того ни с сего\n упал в обморок"};
        Random nomer = new Random();
        

    }

}