using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Cursova_Art
{
    public partial class MainForm : Form
    {
       
        private List<Artist> artists = new List<Artist>();
        private List<Painting> paintings = new List<Painting>();
        private List<Collector> collectors = new List<Collector>();
        private List<Museum> museums = new List<Museum>();
        private List<Auction> auctions = new List<Auction>();
        private List<ConsignmentShop> consignmentShops = new List<ConsignmentShop>();
        private List<Copy> copies = new List<Copy>();
        private List<Review> reviews = new List<Review>();

        private List<Painting> ownedPaintings = new List<Painting>();
        private List<Copy> ownedCopies = new List<Copy>();

        private List<Painting> personalCollection = new List<Painting>();

        private SaveFileDialog saveFileDialog1;
        private OpenFileDialog openFileDialog1;

        private bool painting_filter_flag = false;
        private bool review_filter_flag = false;
        private bool cs_filter_flag = false;
        private bool auction_filter_flag = false;
        private bool museum_filter_flag = false;
        private bool collector_filter_flag = false;

        private bool cs_dc_flag = false;
        private bool review_cb_flag = false;
        private bool auction_dc_flag = false;
        private bool artist_cb_flag = false;
        private bool museum_dc_flag = false;
        private bool collector_dc_flag = false;

        public MainForm()
        {
            InitializeComponent();

            // Initialize SaveFileDialog
            saveFileDialog1 = new SaveFileDialog
            {
                Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*",
                Title = "Save Data"
            };

            // Initialize OpenFileDialog
            openFileDialog1 = new OpenFileDialog
            {
                Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*",
                Title = "Load Data"
            };
        }

        private void HideAllPanels()
        {
            label31.Visible = false;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
            panel8.Visible = false;

            button8.Visible = true;
            button8.Text = "Delete";
            button9.Text = "Cancel";
            button9.Location = new Point(153, 202);
            button4.Location = new Point(12, 245);
            button11.Location = new Point(13, 203);
            button10.Location = new Point(13, 209);
            button7.Location = new Point(36, 206);
            button3.Location = new Point(49, 244);
            button6.Location = new Point(31, 166);
            button5.Location = new Point(31, 200);
            listBox4.SelectionMode = SelectionMode.MultiSimple;
            listBox4.DoubleClick -= new EventHandler(listBox4_DoubleClick);

            label23.Location = new Point(108, 11);

            foreach (Painting painting in paintings)
            {   
                painting.PersonalCollectionFlag = null;
            }

            painting_filter_flag = false;
            review_filter_flag = false;
            cs_filter_flag = false;
            auction_filter_flag = false;
            museum_filter_flag = false;
            collector_filter_flag = false;
        }

        private void SetUp()
        {
            listBox4.Refresh();

            button8.Visible = false;
            button9.Text = "Close";
            button9.Location = new Point(96, 202);
            listBox4.SelectionMode = SelectionMode.One;

            if (listBox4.Items.Count > 0)
            {
                listBox4.DoubleClick += new EventHandler(listBox4_DoubleClick);
            }
        }

        private void PaintingPanelAddPainting(Painting selectedPainting)
        {
            List<Artist> selectedArtist = new List<Artist> { selectedPainting.ArtistName };

            panel2.Visible = true;

            textBox6.Text = selectedPainting.Title;
            numericUpDown2.Value = selectedPainting.Year;
            textBox7.Text = selectedPainting.Style;
            textBox8.Text = selectedPainting.Genre;
            comboBox1.DataSource = selectedArtist;
            numericUpDown4.Value = selectedPainting.Price;

            textBox6.Enabled = false;
            numericUpDown2.Enabled = false;
            textBox7.Enabled = false;
            textBox8.Enabled = false;
            comboBox1.Enabled = true;
            numericUpDown4.Enabled = false;
        }

        private void ArtistPanelSetUp(string Text, bool Flag)
        {
            button3.Text = Text;
            button24.Visible = Flag;

            if (button3.Text == "OK")
            {
                button3.Location = new Point(96, 244);
            }
        }

        private void PaintingPanelSetUp(string Text, bool Flag)
        {
            label32.Visible = false;

            button4.Text = Text;
            button12.Visible = Flag;
            button13.Visible = false;
            button14.Visible = false;
            panel2.Size = new Size(200, 281);
            if (button4.Text == "OK")
            {
                button4.Location = new Point(59, 242);
            }
        }

        private void ReviewPanelSetUp(string Text, bool Flag)
        {
            button11.Text = Text;
            button15.Visible = Flag;
            button16.Visible = false;
            button17.Visible = false;
            panel8.Size = new Size(202, 250);
            if (button11.Text == "OK")
            {
                button11.Location = new Point(59, 208);
            }
        }

        private void CSPanelSetUp(string Text, bool Flag)
        {
            button10.Text = Text;
            button18.Visible = Flag;
            button19.Visible = false;
            button20.Visible = false;
            panel7.Size = new Size(231, 252);
            if (button10.Text == "OK")
            {
                button10.Location = new Point(52, 210);
            }
        }

        private void AuctionPanelSetUp(string Text, bool Flag)
        {
            button7.Text = Text;
            button21.Visible = Flag;
            button22.Visible = false;
            button23.Visible = false;
            panel5.Size = new Size(252, 250);
            if (button7.Text == "OK")
            {
                button7.Location = new Point(85, 208);
            }
        }

        private void MuseumPanelSetUp(string Text, bool Flag)
        {
            button6.Text = Text;
            button25.Visible = Flag;
            button26.Visible = false;
            button27.Visible = false;
            panel4.Size = new Size(228, 208);
            if (button6.Text == "OK")
            {
                button6.Location = new Point(71, 166);
            }
        }

        private void CollectorPanelSetUp(string Text, bool Flag)
        {
            button5.Text = Text;
            button28.Visible = Flag;
            button29.Visible = false;
            button30.Visible = false;
            panel3.Size = new Size(228, 250);
            if (button5.Text == "OK")
            {
                button5.Location = new Point(83, 205);
            }
        }

        private void PaintingPanelEnable()
        {
            label32.Visible = false;

            // Очищуємо текстові поля
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            numericUpDown2.Value = 0;
            numericUpDown4.Value = 0;

            textBox6.Enabled = true;
            numericUpDown2.Enabled = true;
            textBox7.Enabled = true;
            textBox8.Enabled = true;
            comboBox1.Enabled = true;
            numericUpDown4.Enabled = true;
        }

        private void ArtistPanelEnable()
        {
            // Очищуємо текстові поля
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            numericUpDown1.Value = 0;

            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            numericUpDown1.Enabled = true;
            textBox4.Enabled = true;
            textBox5.Enabled = true;
        }

        private void ReviewPanelEnable()
        {
            // Очищуємо текстові поля
            textBox18.Text = "";

            textBox18.Enabled = true;
        }

        private void CSPanelEnable()
        {
            // Очищуємо текстові поля
            textBox16.Text = "";
            textBox17.Text = "";
            numericUpDown3.Value = 0;

            textBox16.Enabled = true;
            textBox17.Enabled = true;
            numericUpDown3.Enabled = true;
        }

        private void AuctionEnable()
        {
            // Очищуємо текстові поля
            textBox14.Text = "";
            textBox15.Text = "";
            dateTimePicker1.Value = DateTime.Now;

            textBox14.Enabled = true;
            textBox15.Enabled = true;
            dateTimePicker1.Enabled = true;
        }

        private void MuseumEnable()
        {
            // Очищуємо текстові поля
            textBox12.Text = "";
            textBox13.Text = "";

            textBox12.Enabled = true;
            textBox13.Enabled = true;
        }

        private void CollectorEnable()
        {
            // Очищуємо текстові поля
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";

            textBox9.Enabled = true;
            textBox11.Enabled = true;
            textBox11.Enabled = true;
        }


        private void artistToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            panel1.Visible = true;

            if (button3.Text == "Find artists")
            {
                button3.Click -= new EventHandler(findArtistButton_Click);
                button3.Click += new EventHandler(button3_Click);
            }

            button3.Text = "Add artist";

            button24.Visible = true;

            // Очищуємо текстові поля
            ArtistPanelEnable();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text == "OK")
            {

                if (artist_cb_flag)
                {
                    ArtistPanelSetUp("OK", false);
                    artist_cb_flag = false;

                    panel1.Visible = false;
                    panel2.Visible = true;
                    return;
                }

                panel1.Visible = false;
                panel6.Visible = true;
                return;
            }

            // Отримуємо дані про художника з текстових полів
            string firstName = textBox1.Text;
            string lastName = textBox2.Text;
            string country = textBox3.Text;
            int birthYear = (int)numericUpDown1.Value;
            string style = textBox4.Text;
            string genre = textBox5.Text;

            // Створюємо нового художника
            Artist newArtist = new Artist(firstName, lastName, country, birthYear, style, genre);

            // Додаємо художника до списку художників
            artists.Add(newArtist);

            ArtistPanelEnable();

            // Приховуємо панель
            panel1.Visible = false;
        }

        private void paintingToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            panel2.Visible = true;

            var sortedArtists = artists.OrderBy(artist => artist.Surname).ThenBy(artist => artist.Name)
                                       .ThenBy(artist => artist.BirthYear).ToList();

            Artist unknownArtist = new Artist("Name", "Unknown", "", 0, "", "");

            // Добавляем объект "Unknown" в начало списка художников
            sortedArtists.Insert(0, unknownArtist);

            comboBox1.DataSource = null; // Сначала сбросьте источник данных комбобокса
            comboBox1.DataSource = sortedArtists; // Затем установите новый источник данных
            comboBox1.Refresh(); // Обновите комбобокс, чтобы отобразить новые данные

            if (button4.Text == "Find paintings")
            {
                button4.Click -= new EventHandler(findPaintingButton_Click);
                button4.Click += new EventHandler(button4_Click);
            }

            button4.Text = "Add painting";

            button12.Visible = true;
            button13.Visible = true;
            button14.Visible = true;
            panel2.Size = new Size(200, 321);

            PaintingPanelEnable();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.Text == "OK")
            {
                panel2.Visible = false;

                if (cs_dc_flag || cs_filter_flag)
                {
                    if (cs_filter_flag)
                    {
                        cs_filter_flag = false;

                        panel7.Visible = true;
                        return;
                    }

                    CSPanelSetUp("OK", false);
                    cs_dc_flag = false;

                    panel7.Visible = true;
                    return;
                }
                else if (review_cb_flag || review_filter_flag)
                {
                    if (review_filter_flag)
                    {
                        review_filter_flag = false;

                        panel8.Visible = true;
                        return;
                    }

                    ReviewPanelSetUp("OK", false);
                    review_cb_flag = false;

                    panel8.Visible = true;
                    return;
                }
                else if (auction_dc_flag || auction_filter_flag)
                {
                    if (auction_filter_flag)
                    {
                        auction_filter_flag = false;

                        panel5.Visible = true;
                        return;
                    }

                    AuctionPanelSetUp("OK", false);
                    auction_dc_flag = false;

                    panel5.Visible = true;
                    return;
                }
                else if (museum_dc_flag || museum_filter_flag)
                {
                    if (museum_filter_flag)
                    {
                        museum_filter_flag = false;

                        panel4.Visible = true;
                        return;
                    }

                    MuseumPanelSetUp("OK", false);
                    museum_dc_flag = false;

                    panel4.Visible = true;
                    return;
                }
                else if (collector_dc_flag || collector_filter_flag)
                {
                    if (collector_filter_flag)
                    {
                        collector_filter_flag = false;

                        panel3.Visible = true;
                        return;
                    }

                    CollectorPanelSetUp("OK", false);
                    collector_dc_flag = false;

                    panel3.Visible = true;
                    return;
                }

                panel6.Visible = true;
                return;
            }

            // Отримуємо дані про картину з текстових полів
            string title = textBox6.Text;
            int year = (int)numericUpDown2.Value;
            string style = textBox7.Text;
            string genre = textBox8.Text;
            Artist artist = (Artist)comboBox1.SelectedItem;
            int price = (int)numericUpDown4.Value;

            // Створюємо нового художника
            Painting newPainting = new Painting(title, year, style, genre, artist, price);

            // Додаємо художника до списку художників
            paintings.Add(newPainting);

            PaintingPanelEnable();

            // Приховуємо панель
            panel2.Visible = false;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            findArtistPanel();

            painting_filter_flag = true;
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;

            if (button3.Text == "Find artists")
            {
                button3.Click -= new EventHandler(findArtistButton_Click);
                button3.Click += new EventHandler(button3_Click);
            }

            ArtistPanelSetUp("OK", false);
            artist_cb_flag = true;

            Artist selectedArtist = (Artist)comboBox1.SelectedItem;

            panel1.Visible = true;

            textBox1.Text = selectedArtist.Name;
            textBox2.Text = selectedArtist.Surname;
            textBox3.Text = selectedArtist.Country;
            numericUpDown1.Value = selectedArtist.BirthYear;
            textBox4.Text = selectedArtist.Style;
            textBox5.Text = selectedArtist.Genre;

            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            numericUpDown1.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
        }

        private void collectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            panel3.Visible = true;

            var freePaintings = paintings.Except(ownedPaintings).ToList();
            var freeCopies = copies.Except(ownedCopies).ToList();
            var sortedPaintings = freePaintings.OrderBy(painting => painting.Title).ThenBy(painting => painting.Year).ToList();
            sortedPaintings.AddRange(freeCopies);

            button5.Text = "Add collector";

            button28.Visible = true;
            button29.Visible = true;
            button30.Visible = true;
            panel3.Size = new Size(228, 271);

            listBox1.DataSource = null; // Сначала сбросьте источник данных комбобокса
            listBox1.DataSource = sortedPaintings; // Затем установите новый источник данных
            listBox1.Refresh(); // Обновите комбобокс, чтобы отобразить новые данные

            CollectorEnable();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Получаем данные о коллекционере из текстовых полей
            string name = textBox9.Text;
            string surname = textBox10.Text;
            List<Painting> pictures = listBox1.SelectedItems.Cast<Painting>().ToList();
            string email = textBox11.Text;

            ownedPaintings.AddRange(pictures);

            // Создаем нового коллекционера
            Collector newCollector = new Collector(name, surname, pictures, email);

            // Очищаем текстовые поля
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";

            // Скрыть панель или закрыть форму, если это необходимо
            panel3.Visible = false;

            if (button5.Text == "OK")
            {
                button5.Text = "Add collector";
                panel6.Visible = true;

                textBox9.Enabled = true;
                textBox10.Enabled = true;
                listBox1.Enabled = true;
                textBox11.Enabled = true;

                SetUp();
            }
            else
            {
                // Добавляем коллекционера в список коллекционеров
                collectors.Add(newCollector);
            }

            CollectorEnable();
        }

        private void button29_Click(object sender, EventArgs e)
        {
            findPaintingPanel();

            collector_filter_flag = true;
        }

        private void listBox1_DoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedItem is null)
            {
                panel3.Visible = true;
                return;
            }

            HideAllPanels();

            collector_dc_flag = true;

            PaintingPanelSetUp("OK", false);

            Painting selectedPainting = (Painting)listBox1.SelectedItem;

            panel2.Visible = true;

            PaintingPanelAddPainting(selectedPainting);

            if (listBox1.SelectedItem is Copy)
            {
                label32.Visible = true;
            }

            if (button5.Text == "Add collector")
            {
                collector_filter_flag = true;
            }
        }

        private void museumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            panel4.Visible = true;

            var freePaintings = paintings.Except(ownedPaintings).ToList();
            var freeCopies = copies.Except(ownedCopies).ToList();
            var sortedPaintings = freePaintings.OrderBy(painting => painting.Title).ThenBy(painting => painting.Year).ToList();
            sortedPaintings.AddRange(freeCopies);

            button6.Text = "Add museum";

            button25.Visible = true;
            button26.Visible = true;
            button27.Visible = true;
            panel4.Size = new Size(228, 235);

            listBox2.DataSource = null; // Сначала сбросьте источник данных комбобокса
            listBox2.DataSource = sortedPaintings; // Затем установите новый источник данных
            listBox2.Refresh(); // Обновите комбобокс, чтобы отобразить новые данные

            MuseumEnable();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Получаем данные о музее из текстовых полей
            string name = textBox12.Text;
            string address = textBox13.Text;
            List<Painting> pictures = listBox2.SelectedItems.Cast<Painting>().ToList();

            ownedPaintings.AddRange(pictures);

            // Создаем новый музей
            Museum newMuseum = new Museum(name, address, pictures);

            // Очищаем текстовые поля
            textBox12.Text = "";
            textBox13.Text = "";

            // Скрыть панель или закрыть форму, если это необходимо
            panel4.Visible = false;

            if (button6.Text == "OK")
            {
                button6.Text = "Add museum";
                panel6.Visible = true;

                textBox12.Enabled = true;
                textBox13.Enabled = true;
                listBox2.Enabled = true;

                SetUp();
            }
            else
            {
                // Добавляем музей в список музеев
                museums.Add(newMuseum);
            }

            MuseumEnable();
        }

        private void button26_Click(object sender, EventArgs e)
        {
            findPaintingPanel();

            museum_filter_flag = true;
        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem is null)
            {
                panel4.Visible = true;
                return;
            }

            HideAllPanels();

            museum_dc_flag = true;

            PaintingPanelSetUp("OK", false);

            Painting selectedPainting = (Painting)listBox2.SelectedItem;

            panel2.Visible = true;

            PaintingPanelAddPainting(selectedPainting);

            if (listBox2.SelectedItem is Copy)
            {
                label32.Visible = true;
            }

            if (button6.Text == "Add museum")
            {
                museum_filter_flag = true;
            }
        }

        private void auctionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            panel5.Visible = true;

            var freePaintings = paintings.Except(ownedPaintings).ToList();
            var freeCopies = copies.Except(ownedCopies).ToList();
            var sortedPaintings = freePaintings.OrderBy(painting => painting.Title).ThenBy(painting => painting.Year).ToList();
            sortedPaintings.AddRange(freeCopies);

            button7.Text = "Add auction";

            button21.Visible = true;
            button22.Visible = true;
            button23.Visible = true;
            panel5.Size = new Size(252, 272);

            listBox3.DataSource = null; // Сначала сбросьте источник данных комбобокса
            listBox3.DataSource = sortedPaintings; // Затем установите новый источник данных
            listBox3.Refresh(); // Обновите комбобокс, чтобы отобразить новые данные

            AuctionEnable();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Получаем данные об аукционе из текстовых полей
            string name = textBox14.Text;
            string address = textBox15.Text;
            DateTime date = dateTimePicker1.Value;
            List<Painting> pictures = listBox3.SelectedItems.Cast<Painting>().ToList();

            ownedPaintings.AddRange(pictures);

            // Создаем нового аукциона
            Auction newAuction = new Auction(name, address, date, pictures);

            // Очищаем текстовые поля
            textBox14.Text = "";
            textBox15.Text = "";

            panel5.Visible = false;

            if (button7.Text == "OK")
            {
                button7.Text = "Add auction";
                panel6.Visible = true;

                textBox14.Enabled = true;
                textBox15.Enabled = true;
                dateTimePicker1.Enabled = true;
                listBox3.Enabled = true;

                SetUp();
            }
            else
            {
                // Добавляем аукцион в список аукционов
                auctions.Add(newAuction);
            }

            AuctionEnable();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            findPaintingPanel();

            auction_filter_flag = true;
        }

        private void listBox3_DoubleClick(object sender, EventArgs e)
        {
            if (listBox3.SelectedItem is null)
            {
                panel5.Visible = true;
                return;
            }

            HideAllPanels();

            auction_dc_flag = true;

            PaintingPanelSetUp("OK", false);

            Painting selectedPainting = (Painting)listBox3.SelectedItem;

            panel2.Visible = true;

            PaintingPanelAddPainting(selectedPainting);

            if (listBox3.SelectedItem is Copy)
            {
                label32.Visible = true;
            }

            if (button7.Text == "Add auction")
            {
                auction_filter_flag = true;
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            panel6.Visible = true;

            var sortedPaintings = paintings.OrderBy(painting => painting.Title).ThenBy(painting => painting.Year).ToList();

            listBox4.DataSource = null; // Сначала сбросьте источник данных комбобокса
            listBox4.DataSource = sortedPaintings; // Затем установите новый источник данных
            listBox4.Refresh(); // Обновите комбобокс, чтобы отобразить новые данные
            button8.Text = "Add copy";
            label23.Text = "Paintings";
            listBox4.SelectionMode = SelectionMode.One;
        }

        private void comissionShopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            panel7.Visible = true;

            var freePaintings = paintings.Except(ownedPaintings).ToList();
            var freeCopies = copies.Except(ownedCopies).ToList();
            var sortedPaintings = freePaintings.OrderBy(painting => painting.Title).ThenBy(painting => painting.Year).ToList();
            var sortedCopies = freeCopies.OrderBy(copy => copy.Title).ThenBy(copy => copy.Year).ToList();
            sortedPaintings.AddRange(sortedCopies);

            button10.Text = "Add consignment shop";

            button18.Visible = true;
            button19.Visible = true;
            button20.Visible = true;
            panel7.Size = new Size(231, 284);

            listBox5.DataSource = null; // Сначала сбросьте источник данных комбобокса
            listBox5.DataSource = sortedPaintings; // Затем установите новый источник данных
            listBox5.Refresh(); // Обновите комбобокс, чтобы отобразить новые данные

            CSPanelEnable();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // Получаем данные о комиссионном магазине из текстовых полей
            string name = textBox16.Text;
            string address = textBox17.Text;
            int comission = (int)numericUpDown3.Value;
            List<Painting> pictures = listBox5.SelectedItems.Cast<Painting>().ToList();

            ownedPaintings.AddRange(pictures);

            // Создаем новый комиссионный магазин
            ConsignmentShop newConsignmentShop = new ConsignmentShop(name, address, comission, paintings);

            // Очищаем текстовые поля
            textBox16.Text = "";
            textBox17.Text = "";
            numericUpDown3.Value = 0;

            // Скрыть панель или закрыть форму, если это необходимо
            panel7.Visible = false;

            if (button10.Text == "OK")
            {
                button10.Text = "Add consignment shop";
                panel6.Visible = true;

                textBox16.Enabled = true;
                textBox17.Enabled = true;
                listBox5.Enabled = true;

                SetUp();
            }
            else
            {
                // Добавляем комиссионный магазин в список комиссионных магазинов
                consignmentShops.Add(newConsignmentShop);
            }

            CSPanelEnable();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            findPaintingPanel();

            cs_filter_flag = true;
        }

        private void listBox5_DoubleClick(object sender, EventArgs e)
        {
            if (listBox5.SelectedItem is null)
            {
                panel7.Visible = true;
                return;
            }

            HideAllPanels();

            cs_dc_flag = true;

            PaintingPanelSetUp("OK", false);

            Painting selectedPainting = (Painting)listBox5.SelectedItem;

            panel2.Visible = true;

            PaintingPanelAddPainting(selectedPainting);

            if (listBox5.SelectedItem is Copy)
            {
                label32.Visible = true;
            }

            if (button10.Text == "Add consignment shop")
            {
                cs_filter_flag = true;
            }
        }

        private void reviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            panel8.Visible = true;

            List<Painting> pictures = reviews.Select(review => review.painting).ToList();
            var freePaintings = paintings.ToList();
            var sortedPaintings = freePaintings.OrderBy(painting => painting.Title).ThenBy(painting => painting.Year).ToList(); 
            sortedPaintings = sortedPaintings.Except(pictures).ToList();

            button11.Text = "Add review";

            button15.Visible = true;
            button16.Visible = true;
            button17.Visible = true;
            panel8.Size = new Size(202, 274);

            comboBox2.DataSource = null; // Сначала сбросьте источник данных комбобокса
            comboBox2.DataSource = sortedPaintings; // Затем установите новый источник данных
            comboBox2.Refresh(); // Обновите комбобокс, чтобы отобразить новые данные

            ReviewPanelEnable();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            // Получаем данные о рецензии из текстового поля
            string reviewText = textBox18.Text;

            Painting picture = (Painting)comboBox2.SelectedItem;

            // Создаем новую рецензию
            Review newReview = new Review(reviewText, picture);

            // Очищаем текстовое поле
            textBox18.Text = "";

            // Скрыть панель или закрыть форму, если это необходимо
            panel8.Visible = false;

            if (button11.Text == "OK")
            {
                button11.Text = "Add review";
                //button15.Visible = true;
                //button16.Visible = true;
                //button17.Visible = true;
                //panel8.Size = new Size(202, 274);
                panel6.Visible = true;

                textBox18.Enabled = true;
                comboBox2.Enabled = true;

                SetUp();
            }
            else
            {
                // Добавляем рецензию в список рецензий
                reviews.Add(newReview);
            }

            ReviewPanelEnable();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            findPaintingPanel();

            review_filter_flag = true;
        }

        private void comboBox2_Click(object sender, EventArgs e)
        {
            panel8.Visible = false;

            if (button4.Text == "Find paintings")
            {
                button4.Click -= new EventHandler(findPaintingButton_Click);
                button4.Click += new EventHandler(button4_Click);
            }

            PaintingPanelSetUp("OK", false);

            review_cb_flag = true;

            Painting selectedPainting = (Painting)comboBox2.SelectedItem;

            panel2.Visible = true;

            PaintingPanelAddPainting(selectedPainting);

            if (button11.Text == "Add review")
            {
                review_filter_flag = true;
            }
        }

        private void paintingToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            panel6.Visible = true;

            var sortedPaintings = paintings.OrderBy(painting => painting.Title).ThenBy(painting => painting.Year).ToList();
            sortedPaintings.AddRange(copies);

            listBox4.DataSource = null; // Сначала сбросьте источник данных комбобокса
            listBox4.DataSource = sortedPaintings; // Затем установите новый источник данных
            listBox4.Refresh(); // Обновите комбобокс, чтобы отобразить новые данные

            label23.Text = "Paintings";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (button8.Text == "Add copy")
            {
                Painting painting = (Painting)listBox4.SelectedItem;
                Copy copy = new Copy(painting.Title, painting.Year, painting.Style, painting.Genre, painting.ArtistName, painting.Price);

                copies.Add(copy);
                button9.Text = "Delete";
            }
            else if (button8.Text == "Buy")
            {
                List<Painting> pictures = listBox4.SelectedItems.Cast<Painting>().ToList();
                personalCollection.AddRange(pictures);
                ownedPaintings.AddRange(pictures);

                button9.Text = "Delete";
            }
            else if (button8.Text == "Sell")
            {
                List<Painting> pictures = listBox4.SelectedItems.Cast<Painting>().ToList();
                personalCollection = personalCollection.Except(pictures).ToList();
                ownedPaintings = ownedPaintings.Except(pictures).ToList();

                button9.Text = "Delete";
            }
            else if (label23.Text == "Paintings")
            {
                List<Painting> pictures = listBox4.SelectedItems.Cast<Painting>().ToList();

                // Создаем копию коллекции для перебора
                List<Painting> picturesToRemove = new List<Painting>(pictures);

                foreach (Painting picture in picturesToRemove)
                {
                    paintings.Remove(picture);
                }
            }
            else if (label23.Text == "Artists")
            {
                List<Artist> selectedArtists = listBox4.SelectedItems.Cast<Artist>().ToList();

                // Создаем копию коллекции для перебора
                List<Artist> artistsToRemove = new List<Artist>(selectedArtists);

                foreach (Artist artist in artistsToRemove)
                {
                    artists.Remove(artist);
                }
            }
            else if (label23.Text == "Collectors")
            {
                List<Collector> selectedCollectors = listBox4.SelectedItems.Cast<Collector>().ToList();

                // Создаем копию коллекции для перебора
                List<Collector> collectorsToRemove = new List<Collector>(selectedCollectors);

                foreach (Collector collector in collectorsToRemove)
                {
                    collectors.Remove(collector);
                }
            }
            else if (label23.Text == "Museums")
            {
                List<Museum> selectedMuseums = listBox4.SelectedItems.Cast<Museum>().ToList();

                // Создаем копию коллекции для перебора
                List<Museum> museumsToRemove = new List<Museum>(selectedMuseums);

                foreach (Museum museum in museumsToRemove)
                {
                    museums.Remove(museum);
                }
            }
            else if (label23.Text == "Auctions")
            {
                List<Auction> selectedAuctions = listBox4.SelectedItems.Cast<Auction>().ToList();

                // Создаем копию коллекции для перебора
                List<Auction> auctionsToRemove = new List<Auction>(selectedAuctions);

                foreach (Auction auction in auctionsToRemove)
                {
                    auctions.Remove(auction);
                }
            }
            else if (label23.Text == "Reviews")
            {
                List<Review> selectedReviews = listBox4.SelectedItems.Cast<Review>().ToList();

                // Создаем копию коллекции для перебора
                List<Review> reviewsToRemove = new List<Review>(selectedReviews);

                foreach (Review review in reviewsToRemove)
                {
                    reviews.Remove(review);
                }
            }

            panel6.Visible = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (painting_filter_flag)
            {
                HideAllPanels();
                panel2.Visible = true;
                return;
            }
            else if (review_filter_flag)
            {
                HideAllPanels();
                panel8.Visible = true;
                return;
            }
            else if (cs_filter_flag)
            {
                HideAllPanels();
                panel7.Visible = true;
                return;
            }
            else if (auction_filter_flag)
            {
                HideAllPanels();
                panel5.Visible = true;
                return;
            }
            else if (museum_filter_flag)
            {
                HideAllPanels();
                panel4.Visible = true;
                return;
            }
            else if (collector_filter_flag)
            {
                HideAllPanels();
                panel3.Visible = true;
                return;
            }
            else
            {
                HideAllPanels();
            }
        }

        private void artistToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            panel6.Visible = true;

            var sortedArtists = artists.OrderBy(artist => artist.Surname).ThenBy(artist => artist.Name)
                                       .ThenBy(artist => artist.BirthYear).ToList();

            listBox4.DataSource = null; // Сначала сбросьте источник данных комбобокса
            listBox4.DataSource = sortedArtists; // Затем установите новый источник данных
            listBox4.Refresh(); // Обновите комбобокс, чтобы отобразить новые данные

            label23.Text = "Artists";
        }

        private void collectionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            panel6.Visible = true;

            var sortedCollectors = collectors.OrderBy(collector => collector.Surname).ThenBy(collector => collector.Name).ToList();

            listBox4.DataSource = null; // Сначала сбросьте источник данных комбобокса
            listBox4.DataSource = sortedCollectors; // Затем установите новый источник данных
            listBox4.Refresh(); // Обновите комбобокс, чтобы отобразить новые данные

            label23.Text = "Collectors";
        }

        private void museumToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            panel6.Visible = true;

            var sortedMuseums = museums.OrderBy(museum => museum.Name).ToList();

            listBox4.DataSource = null; // Сначала сбросьте источник данных комбобокса
            listBox4.DataSource = sortedMuseums; // Затем установите новый источник данных
            listBox4.Refresh(); // Обновите комбобокс, чтобы отобразить новые данные

            label23.Text = "Museums";
        }

        private void auctionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            panel6.Visible = true;

            var sortedAuctions = auctions.OrderBy(auction => auction.Name).ToList();

            listBox4.DataSource = null; // Сначала сбросьте источник данных комбобокса
            listBox4.DataSource = sortedAuctions; // Затем установите новый источник данных
            listBox4.Refresh(); // Обновите комбобокс, чтобы отобразить новые данные

            label23.Text = "Auctions";
        }

        private void comissionShopToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            panel6.Visible = true;

            var sortedConsignmentShops = consignmentShops.OrderBy(consignmentShop => consignmentShop.Name).ToList();

            listBox4.DataSource = null; // Сначала сбросьте источник данных комбобокса
            listBox4.DataSource = sortedConsignmentShops; // Затем установите новый источник данных
            listBox4.Refresh(); // Обновите комбобокс, чтобы отобразить новые данные

            label23.Text = "Consignment shops";
            label23.Location = new Point(88, 11);
        }

        private void reviewToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            panel6.Visible = true;

            var sortedReviews = reviews.OrderBy(review => review.Text).ToList();

            listBox4.DataSource = null; // Сначала сбросьте источник данных комбобокса
            listBox4.DataSource = sortedReviews; // Затем установите новый источник данных
            listBox4.Refresh(); // Обновите комбобокс, чтобы отобразить новые данные

            label23.Text = "Reviews";
        }
        private void paintingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            panel6.Visible = true;

            label23.Text = "Paintings";

            var sortedPaintings = paintings.OrderBy(painting => painting.Title).ThenBy(painting => painting.Year).ToList();
            sortedPaintings.AddRange(copies);

            listBox4.DataSource = null;
            listBox4.DataSource = sortedPaintings;

            SetUp();
        }

        private void listBox4_DoubleClick(object sender, EventArgs e)
        {
            panel6.Visible = false;

            if (label23.Text == "Paintings")
            {
                if (button4.Text == "Find paintings")
                {
                    button4.Click -= new EventHandler(findPaintingButton_Click);
                    button4.Click += new EventHandler(button4_Click);
                }

                PaintingPanelSetUp("OK", false);

                Painting selectedPainting = (Painting)listBox4.SelectedItem;

                panel2.Visible = true;

                PaintingPanelAddPainting(selectedPainting);

                if (listBox4.SelectedItem is Copy)
                {
                    label32.Visible = true;
                }
            }
            else if (label23.Text == "Artists")
            {
                if (button3.Text == "Find artists")
                {
                    button3.Click -= new EventHandler(findArtistButton_Click);
                    button3.Click += new EventHandler(button3_Click);
                }

                ArtistPanelSetUp("OK", false);

                Artist selectedArtist = (Artist)listBox4.SelectedItem;

                panel1.Visible = true;

                textBox1.Text = selectedArtist.Name;
                textBox2.Text = selectedArtist.Surname;
                textBox3.Text = selectedArtist.Country;
                numericUpDown1.Value = selectedArtist.BirthYear;
                textBox4.Text = selectedArtist.Style;
                textBox5.Text = selectedArtist.Genre;

                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                numericUpDown1.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
            }
            else if (label23.Text == "Collectors")
            {
                CollectorPanelSetUp("OK", false);

                Collector selectedCollector = (Collector)listBox4.SelectedItem;

                panel3.Visible = true;

                textBox9.Text = selectedCollector.Name;
                textBox10.Text = selectedCollector.Surname;
                listBox1.DataSource = selectedCollector.Collection;
                listBox1.Refresh();
                textBox11.Text = selectedCollector.Email;

                textBox9.Enabled = false;
                textBox10.Enabled = false;
                textBox11.Enabled = false;

                collector_dc_flag = true;
            }
            else if (label23.Text == "Museums")
            {
                MuseumPanelSetUp("OK", false);

                Museum selectedMuseum = (Museum)listBox4.SelectedItem;

                panel4.Visible = true;

                textBox12.Text = selectedMuseum.Name;
                textBox13.Text = selectedMuseum.Address;
                listBox2.DataSource = selectedMuseum.Paintings;
                listBox2.Refresh();

                textBox12.Enabled = false;
                textBox13.Enabled = false;  
            }
            else if (label23.Text == "Auctions")
            {
                AuctionPanelSetUp("OK", false);

                Auction selectedAuction = (Auction)listBox4.SelectedItem;

                panel5.Visible = true;

                textBox14.Text = selectedAuction.Name;
                textBox15.Text = selectedAuction.Address;
                dateTimePicker1.Value = selectedAuction.Date;
                listBox3.DataSource = selectedAuction.Lots;
                listBox3.Refresh();

                textBox14.Enabled = false;
                textBox15.Enabled = false;
                dateTimePicker1.Enabled = false;
            }   
            else if (label23.Text == "Consignment shops")
            {
                CSPanelSetUp("OK", false);

                ConsignmentShop selectedConsignmentShop = (ConsignmentShop)listBox4.SelectedItem;

                panel7.Visible = true;

                textBox16.Text = selectedConsignmentShop.Name;
                textBox17.Text = selectedConsignmentShop.Address;
                numericUpDown3.Value = selectedConsignmentShop.Comission;
                listBox5.DataSource = null;
                listBox5.DataSource = selectedConsignmentShop.Paintings;
                listBox5.Refresh();

                textBox16.Enabled = false;
                textBox17.Enabled = false;
                numericUpDown3.Enabled = false;
            }
            else if (label23.Text == "Reviews")
            {
                ReviewPanelSetUp("OK", false);

                Review selectedReview = (Review)listBox4.SelectedItem;
                List<Painting> selectedPainting = new List<Painting> { selectedReview.painting };

                panel8.Visible = true;

                textBox18.Text = selectedReview.Text;
                comboBox2.DataSource = selectedPainting;
                comboBox2.Refresh();

                textBox18.Enabled = false;
            }
            else if (label23.Text == "Personal collection")
            {
                if (button4.Text == "Find paintings")
                {
                    button4.Click -= new EventHandler(findPaintingButton_Click);
                    button4.Click += new EventHandler(button4_Click);
                }

                PaintingPanelSetUp("OK", false);

                Painting selectedPainting = (Painting)listBox4.SelectedItem;

                panel2.Visible = true;

                PaintingPanelAddPainting(selectedPainting);
            }
        }

        private void artistsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            panel6.Visible = true;

            label23.Text = "Artists";

            var sortedArtists = artists.OrderBy(artist => artist.Surname).ThenBy(artist => artist.Name)
                                       .ThenBy(artist => artist.BirthYear).ToList();

            listBox4.DataSource = null;
            listBox4.DataSource = sortedArtists;

            SetUp();
        }

        private void collectorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            panel6.Visible = true;

            label23.Text = "Collectors";

            var sortedCollectors = collectors.OrderBy(collector => collector.Surname).ThenBy(collector => collector.Name).ToList();

            listBox4.DataSource = null;
            listBox4.DataSource = sortedCollectors;

            SetUp();
        }

        private void museumsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            panel6.Visible = true;

            label23.Text = "Museums";

            var sortedMuseums = museums.OrderBy(museum => museum.Name).ToList();

            listBox4.DataSource = null;
            listBox4.DataSource = sortedMuseums;

            SetUp();
        }

        private void auctionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            panel6.Visible = true;

            label23.Text = "Auctions";

            var sortedAuctions = auctions.OrderBy(auction => auction.Name).ToList();

            listBox4.DataSource = null;
            listBox4.DataSource = sortedAuctions;

            SetUp();
        }

        private void comissionShopToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            panel6.Visible = true;

            label23.Text = "Consignment shops";
            label23.Location = new Point(88, 11);

            var sortedConsignmentShops = consignmentShops.OrderBy(consignmentShop => consignmentShop.Name).ToList();

            listBox4.DataSource = null;
            listBox4.DataSource = sortedConsignmentShops;

            SetUp();
        }

        private void reviewsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            panel6.Visible = true;

            label23.Text = "Reviews";

            var sortedReviews = reviews.OrderBy(review => review.painting.Title).ToList();

            listBox4.DataSource = null;
            listBox4.DataSource = sortedReviews;

            SetUp();
        }

        private void findPaintingPanel()
        {
            PaintingPanelEnable();
              
            HideAllPanels();
            panel2.Visible = true;

            var sortedArtists = artists.OrderBy(artist => artist.Surname).ThenBy(artist => artist.Name)
                           .ThenBy(artist => artist.BirthYear).ToList();

            Artist unknownArtist = new Artist("Name", "Unknown", "", 0, "", "");

            // Добавляем объект "Unknown" в начало списка художников
            sortedArtists.Insert(0, unknownArtist);

            comboBox1.DataSource = sortedArtists;

            if (button4.Text != "Find paintings")
            {
                PaintingPanelSetUp("Find paintings", true);
                button4.Click -= new EventHandler(button4_Click);
                button4.Click += new EventHandler(findPaintingButton_Click);
            }
        }

        private void findPaintingButton_Click(object sender, EventArgs e)
        {
            // Отримуємо дані про картину з текстових полів
            string title = textBox6.Text;
            int year = (int)numericUpDown2.Value;
            string style = textBox7.Text;
            string genre = textBox8.Text;
            Artist artist = (Artist)comboBox1.SelectedItem;
            int price = (int)numericUpDown4.Value;

            // Очищуємо текстові поля
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            numericUpDown2.Value = 0;

            // Приховуємо панель
            panel2.Visible = false;

            // Пошук картин в списку картин
            List<Painting> foundPaintings = Painting.FindPaintings(paintings, title, year, style, genre, artist, price);

            // Показать результаты поиска
            panel6.Visible = true;
            label23.Text = "Paintings";
            listBox4.DataSource = null;
            listBox4.DataSource = foundPaintings;

            if (review_filter_flag)
            {
                List<Painting> pictures = reviews.Select(review => review.painting).ToList();
                List<Painting> filteredPaintings = foundPaintings.Except(pictures).ToList();
                filteredPaintings = filteredPaintings.Where(painting => !(painting is Copy)).ToList();

                comboBox2.DataSource = null; // Сначала сбросьте источник данных комбобокса
                comboBox2.DataSource = filteredPaintings; // Затем установите новый источник данных
                comboBox2.Refresh(); // Обновите комбобокс, чтобы отобразить новые данные

                listBox4.DataSource = null;
                listBox4.DataSource = filteredPaintings;
            }
            else if (cs_filter_flag)
            {
                List<Painting> filteredPaintings = foundPaintings.Except(ownedPaintings).ToList();

                listBox5.DataSource = null; // Сначала сбросьте источник данных комбобокса
                listBox5.DataSource = filteredPaintings; // Затем установите новый источник данных
                listBox5.Refresh(); // Обновите комбобокс, чтобы отобразить новые данные

                listBox4.DataSource = null;
                listBox4.DataSource = filteredPaintings;
            }
            else if (auction_filter_flag)
            {
                List<Painting> filteredPaintings = foundPaintings.Except(ownedPaintings).ToList();

                listBox3.DataSource = null; // Сначала сбросьте источник данных комбобокса
                listBox3.DataSource = filteredPaintings; // Затем установите новый источник данных
                listBox3.Refresh(); // Обновите комбобокс, чтобы отобразить новые данные

                listBox4.DataSource = null;
                listBox4.DataSource = filteredPaintings;
            }
            else if (museum_filter_flag)
            {
                List<Painting> filteredPaintings = foundPaintings.Except(ownedPaintings).ToList();

                listBox2.DataSource = null; // Сначала сбросьте источник данных комбобокса
                listBox2.DataSource = filteredPaintings; // Затем установите новый источник данных
                listBox2.Refresh(); // Обновите комбобокс, чтобы отобразить новые данные

                listBox4.DataSource = null;
                listBox4.DataSource = filteredPaintings;
            }
            else if (collector_filter_flag)
            {
                List<Painting> filteredPaintings = foundPaintings.Except(ownedPaintings).ToList();

                listBox1.DataSource = null; // Сначала сбросьте источник данных комбобокса
                listBox1.DataSource = filteredPaintings; // Затем установите новый источник данных
                listBox1.Refresh(); // Обновите комбобокс, чтобы отобразить новые данные

                listBox4.DataSource = null;
                listBox4.DataSource = filteredPaintings;
            }

            SetUp();
        }

        private void findArtistPanel()
        {
            ArtistPanelEnable();

            HideAllPanels();
            panel1.Visible = true;

            if (button3.Text != "Find artists")
            {
                ArtistPanelSetUp("Find artists", true);
                button3.Click -= new EventHandler(button3_Click);
                button3.Click += new EventHandler(findArtistButton_Click);
            }
        }

        private void findArtistButton_Click(object sender, EventArgs e)
        {
            // Отримуємо дані про художника з текстових полів
            string firstName = textBox1.Text;
            string lastName = textBox2.Text;
            string country = textBox3.Text;
            int birthYear = (int)numericUpDown1.Value;
            string style = textBox4.Text;
            string genre = textBox5.Text;

            // Очищуємо текстові поля
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            numericUpDown1.Value = 0;
            textBox4.Text = "";
            textBox5.Text = "";

            // Приховуємо панель
            panel1.Visible = false;

            // Пошук художників в списку художників
            List<Artist> foundArtists = Artist.FindArtists(artists, firstName, lastName, country, birthYear, style, genre);

            // Показати результати пошуку
            panel6.Visible = true;
            label23.Text = "Artists";
            listBox4.DataSource = null;
            listBox4.DataSource = foundArtists;

            if (painting_filter_flag)
            {
                Artist unknownArtist = new Artist("Name", "Unknown", "", 0, "", "");

                // Добавляем объект "Unknown" в начало списка художников
                foundArtists.Insert(0, unknownArtist);

                comboBox1.DataSource = null; // Сначала сбросьте источник данных комбобокса
                comboBox1.DataSource = foundArtists; // Затем установите новый источник данных
                comboBox1.Refresh(); // Обновите комбобокс, чтобы отобразить новые данные
            }

            SetUp();
        }

        private void buyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            panel6.Visible = true;

            label23.Text = "Paintings";
            button8.Text = "Buy";

            var freePaintings = paintings.Except(ownedPaintings).ToList();
            var freeCopies = copies.Except(ownedCopies).ToList();
            var sortedPaintings = freePaintings.OrderBy(painting => painting.Title).ThenBy(painting => painting.Year).ToList();
            sortedPaintings.AddRange(freeCopies);

            foreach (Painting painting in sortedPaintings)
            {
                painting.PersonalCollectionFlag = "Buy";
            }

            listBox4.DataSource = null; // Сначала сбросьте источник данных комбобокса
            listBox4.DataSource = sortedPaintings; // Затем установите новый источник данных
            listBox4.Refresh(); // Обновите комбобокс, чтобы отобразить новые данные
        }

        private void sellToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            panel6.Visible = true;

            label23.Text = "Paintings";
            button8.Text = "Sell";

            var sortedPaintings = personalCollection.OrderBy(painting => painting.Title).ThenBy(painting => painting.Year).ToList();

            foreach (Painting painting in sortedPaintings)
            {
                painting.PersonalCollectionFlag = "Sell";
            }

            listBox4.DataSource = null; // Сначала сбросьте источник данных комбобокса
            listBox4.DataSource = sortedPaintings; // Затем установите новый источник данных
            listBox4.Refresh(); // Обновите комбобокс, чтобы отобразить новые данные
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            panel6.Visible = true;

            label23.Text = "Personal collection";
            label23.Location = new Point(88, 11);

            var sortedPaintings = personalCollection.OrderBy(painting => painting.Title).ThenBy(painting => painting.Year).ToList();

            listBox4.DataSource = null;
            listBox4.DataSource = sortedPaintings;

            SetUp();
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            HideAllPanels();

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                DataContainer dataContainer = new DataContainer(artists, paintings, collectors, museums, auctions,
                                                                consignmentShops, copies, reviews, personalCollection);

                dataContainer.SaveToFile(saveFileDialog1.FileName);
            }
        }

        private void loadToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            HideAllPanels();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;
                DataContainer dataContainer = DataContainer.LoadFromFile(filePath);

                if (dataContainer != null)
                {
                    artists = dataContainer.Artists;
                    paintings = dataContainer.Paintings;
                    collectors = dataContainer.Collectors;
                    museums = dataContainer.Museums;
                    auctions = dataContainer.Auctions;
                    consignmentShops = dataContainer.ConsignmentShops;
                    copies = dataContainer.Copies;
                    reviews = dataContainer.Reviews;
                    personalCollection = dataContainer.PersonalCollection;

                    // Обновление локальных списков картин и копий в собственности
                    ownedPaintings = dataContainer.OwnedPaintings;
                    ownedCopies = dataContainer.OwnedCopies;
                }
                else
                {
                    MessageBox.Show("Failed to load data.");
                }
            }
        }

        private void aboutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            HideAllPanels();
            label31.Visible = true;
        }
    }


}
