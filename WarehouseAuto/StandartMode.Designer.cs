namespace WarehouseAuto
{
    partial class StandartMode
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.EnterInvoice = new System.Windows.Forms.ListBox();
            this.HistoryInvoice = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.PrintMeshok = new System.Windows.Forms.Button();
            this.AutoMark = new System.Windows.Forms.CheckBox();
            this.EmulateCheckBox = new System.Windows.Forms.CheckBox();
            this.InvoceCountText = new System.Windows.Forms.Label();
            this.Reaction = new System.Windows.Forms.CheckBox();
            this.AutoAddButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.OldMode = new System.Windows.Forms.CheckBox();
            this.containerIDUpDown = new System.Windows.Forms.NumericUpDown();
            this.ContainerComboBox = new System.Windows.Forms.ComboBox();
            this.SetPortButton = new System.Windows.Forms.Button();
            this.comText = new System.Windows.Forms.MaskedTextBox();
            this.StateText = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SelectionCount = new System.Windows.Forms.Label();
            this.MStateText = new System.Windows.Forms.Label();
            this.ConsoList = new System.Windows.Forms.ListBox();
            this.AddConsolidations = new System.Windows.Forms.Button();
            this.ButtonsPanel = new System.Windows.Forms.Button();
            this.ConsolidationCount = new System.Windows.Forms.Label();
            this.CreateConsolidation = new System.Windows.Forms.Button();
            this.CreatePackage = new System.Windows.Forms.Button();
            this.CreatePallet = new System.Windows.Forms.Button();
            this.TextWindowButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.GTMField = new System.Windows.Forms.MaskedTextBox();
            this.SealField = new System.Windows.Forms.MaskedTextBox();
            this.AutoOpenGMX = new System.Windows.Forms.Button();
            this.OpenCVToggle = new System.Windows.Forms.CheckBox();
            this.SetSealButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.containerIDUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // EnterInvoice
            // 
            this.EnterInvoice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.EnterInvoice.BackColor = System.Drawing.Color.White;
            this.EnterInvoice.FormattingEnabled = true;
            this.EnterInvoice.Location = new System.Drawing.Point(0, 90);
            this.EnterInvoice.Name = "EnterInvoice";
            this.EnterInvoice.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.EnterInvoice.Size = new System.Drawing.Size(249, 147);
            this.EnterInvoice.TabIndex = 0;
            this.EnterInvoice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EnterInvoice_KeyDown);
            // 
            // HistoryInvoice
            // 
            this.HistoryInvoice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.HistoryInvoice.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.HistoryInvoice.FormattingEnabled = true;
            this.HistoryInvoice.Location = new System.Drawing.Point(1, 274);
            this.HistoryInvoice.Name = "HistoryInvoice";
            this.HistoryInvoice.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.HistoryInvoice.Size = new System.Drawing.Size(248, 69);
            this.HistoryInvoice.TabIndex = 1;
            this.HistoryInvoice.SelectedIndexChanged += new System.EventHandler(this.HistoryInvoice_SelectedIndexChanged);
            this.HistoryInvoice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HistoryInvoice_KeyDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.SetSealButton);
            this.panel1.Controls.Add(this.PrintMeshok);
            this.panel1.Controls.Add(this.AutoMark);
            this.panel1.Controls.Add(this.EmulateCheckBox);
            this.panel1.Controls.Add(this.InvoceCountText);
            this.panel1.Controls.Add(this.Reaction);
            this.panel1.Controls.Add(this.AutoAddButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(255, 62);
            this.panel1.TabIndex = 2;
            // 
            // PrintMeshok
            // 
            this.PrintMeshok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.PrintMeshok.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PrintMeshok.Location = new System.Drawing.Point(227, 5);
            this.PrintMeshok.Name = "PrintMeshok";
            this.PrintMeshok.Size = new System.Drawing.Size(22, 23);
            this.PrintMeshok.TabIndex = 20;
            this.PrintMeshok.Text = "М";
            this.PrintMeshok.UseVisualStyleBackColor = true;
            this.PrintMeshok.Click += new System.EventHandler(this.PrintMeshok_Click);
            // 
            // AutoMark
            // 
            this.AutoMark.AutoSize = true;
            this.AutoMark.Location = new System.Drawing.Point(158, 44);
            this.AutoMark.Name = "AutoMark";
            this.AutoMark.Size = new System.Drawing.Size(89, 17);
            this.AutoMark.TabIndex = 5;
            this.AutoMark.Text = "Маркировка";
            this.AutoMark.UseVisualStyleBackColor = true;
            // 
            // EmulateCheckBox
            // 
            this.EmulateCheckBox.AutoSize = true;
            this.EmulateCheckBox.Location = new System.Drawing.Point(158, 27);
            this.EmulateCheckBox.Name = "EmulateCheckBox";
            this.EmulateCheckBox.Size = new System.Drawing.Size(76, 17);
            this.EmulateCheckBox.TabIndex = 3;
            this.EmulateCheckBox.Text = "Эмуляция";
            this.EmulateCheckBox.UseVisualStyleBackColor = true;
            // 
            // InvoceCountText
            // 
            this.InvoceCountText.AutoSize = true;
            this.InvoceCountText.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.InvoceCountText.Location = new System.Drawing.Point(99, 31);
            this.InvoceCountText.Name = "InvoceCountText";
            this.InvoceCountText.Size = new System.Drawing.Size(30, 31);
            this.InvoceCountText.TabIndex = 4;
            this.InvoceCountText.Text = "0";
            // 
            // Reaction
            // 
            this.Reaction.AutoSize = true;
            this.Reaction.Checked = true;
            this.Reaction.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Reaction.Location = new System.Drawing.Point(158, 9);
            this.Reaction.Name = "Reaction";
            this.Reaction.Size = new System.Drawing.Size(69, 17);
            this.Reaction.TabIndex = 2;
            this.Reaction.Text = "Реакция";
            this.Reaction.UseVisualStyleBackColor = true;
            // 
            // AutoAddButton
            // 
            this.AutoAddButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AutoAddButton.Location = new System.Drawing.Point(12, 5);
            this.AutoAddButton.Name = "AutoAddButton";
            this.AutoAddButton.Size = new System.Drawing.Size(140, 23);
            this.AutoAddButton.TabIndex = 0;
            this.AutoAddButton.Text = "Воспроизведение";
            this.AutoAddButton.UseVisualStyleBackColor = true;
            this.AutoAddButton.Click += new System.EventHandler(this.AutoAddButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(3, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "В процессе:";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.panel2.Controls.Add(this.OldMode);
            this.panel2.Controls.Add(this.containerIDUpDown);
            this.panel2.Controls.Add(this.ContainerComboBox);
            this.panel2.Controls.Add(this.SetPortButton);
            this.panel2.Controls.Add(this.comText);
            this.panel2.Controls.Add(this.StateText);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 430);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(255, 53);
            this.panel2.TabIndex = 3;
            // 
            // OldMode
            // 
            this.OldMode.AutoSize = true;
            this.OldMode.Location = new System.Drawing.Point(71, 31);
            this.OldMode.Name = "OldMode";
            this.OldMode.Size = new System.Drawing.Size(77, 17);
            this.OldMode.TabIndex = 6;
            this.OldMode.Text = "Медленно";
            this.OldMode.UseVisualStyleBackColor = true;
            // 
            // containerIDUpDown
            // 
            this.containerIDUpDown.Location = new System.Drawing.Point(0, 4);
            this.containerIDUpDown.Name = "containerIDUpDown";
            this.containerIDUpDown.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.containerIDUpDown.Size = new System.Drawing.Size(40, 20);
            this.containerIDUpDown.TabIndex = 10;
            this.containerIDUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.containerIDUpDown.ValueChanged += new System.EventHandler(this.containerIDUpDown_ValueChanged);
            // 
            // ContainerComboBox
            // 
            this.ContainerComboBox.AutoCompleteCustomSource.AddRange(new string[] {
            "Основное",
            "Москва местами",
            "Складочная",
            "Дубровка",
            "Томилино",
            "Москва ЕЛИ местами",
            "Складочная ЕЛИ",
            "Дубровка ЕЛИ",
            "Томилино ЕЛИ",
            "Хабаровск местами",
            "Хабаровск мешок",
            "Южный местами",
            "Южный мешок",
            "Камчатк местами",
            "Камчатка мешок",
            "Флагман",
            "Арсеньев",
            "Спасск",
            "Лесозаводск",
            "Дальнереченск",
            "Дальнегорск",
            "Москва АМО-ПРЕСС паллет",
            "Москва АМО-ПРЕСС мешок",
            "Южный ДВ ТЭК паллет",
            "Южный ДВ ТЭК мешок",
            "Камчатка ДВ ТЭК паллет",
            "Камчатка ДВ ТЭК мешок",
            "Магадан ДВ ТЭК паллет",
            "Магадан ДВ ТЭК мешок"});
            this.ContainerComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ContainerComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ContainerComboBox.FormattingEnabled = true;
            this.ContainerComboBox.Items.AddRange(new object[] {
            "Основное",
            "Москва местами",
            "Складочная",
            "Дубровка",
            "Томилино",
            "Сверхсрочки",
            "БИО",
            "Москва ЕЛИ местами",
            "Складочная ЕЛИ",
            "Дубровка ЕЛИ",
            "Томилино ЕЛИ",
            "Хабаровск местами",
            "Хабаровск мешок",
            "Южный местами",
            "Южный мешок",
            "Камчатка местами",
            "Камчатка мешок",
            "Артем места",
            "Артем мешок",
            "Флагман",
            "Флагман мешок",
            "Арсеньев",
            "Спасск",
            "Лесозаводск",
            "Дальнереченск",
            "Дальнегорск",
            "Москва АМО-ПРЕСС паллет",
            "Москва АМО-ПРЕСС мешок",
            "Южный ДВ ТЭК паллет",
            "Южный ДВ ТЭК мешок",
            "Камчатка ДВ ТЭК паллет",
            "Камчатка ДВ ТЭК мешок",
            "Магадан ДВ ТЭК паллет",
            "Магадан ДВ ТЭК мешок"});
            this.ContainerComboBox.Location = new System.Drawing.Point(41, 4);
            this.ContainerComboBox.Name = "ContainerComboBox";
            this.ContainerComboBox.Size = new System.Drawing.Size(173, 21);
            this.ContainerComboBox.TabIndex = 6;
            this.ContainerComboBox.Text = "Основное";
            this.ContainerComboBox.SelectedIndexChanged += new System.EventHandler(this.ContainerComboBox_SelectedIndexChanged);
            // 
            // SetPortButton
            // 
            this.SetPortButton.Location = new System.Drawing.Point(199, 29);
            this.SetPortButton.Name = "SetPortButton";
            this.SetPortButton.Size = new System.Drawing.Size(50, 20);
            this.SetPortButton.TabIndex = 5;
            this.SetPortButton.Text = "PORT";
            this.SetPortButton.UseVisualStyleBackColor = true;
            this.SetPortButton.Click += new System.EventHandler(this.SetPortButton_Click);
            // 
            // comText
            // 
            this.comText.Location = new System.Drawing.Point(150, 29);
            this.comText.Name = "comText";
            this.comText.Size = new System.Drawing.Size(43, 20);
            this.comText.TabIndex = 4;
            this.comText.Text = "COM3";
            this.comText.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.comText_MaskInputRejected);
            // 
            // StateText
            // 
            this.StateText.AutoSize = true;
            this.StateText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StateText.Location = new System.Drawing.Point(9, 33);
            this.StateText.Name = "StateText";
            this.StateText.Size = new System.Drawing.Size(60, 13);
            this.StateText.TabIndex = 3;
            this.StateText.Text = "Приемка";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 258);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "История:";
            // 
            // SelectionCount
            // 
            this.SelectionCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SelectionCount.AutoSize = true;
            this.SelectionCount.BackColor = System.Drawing.SystemColors.Control;
            this.SelectionCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SelectionCount.Location = new System.Drawing.Point(66, 245);
            this.SelectionCount.Name = "SelectionCount";
            this.SelectionCount.Size = new System.Drawing.Size(23, 25);
            this.SelectionCount.TabIndex = 5;
            this.SelectionCount.Text = "0";
            // 
            // MStateText
            // 
            this.MStateText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.MStateText.AutoSize = true;
            this.MStateText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MStateText.ForeColor = System.Drawing.Color.Black;
            this.MStateText.Location = new System.Drawing.Point(95, 249);
            this.MStateText.Name = "MStateText";
            this.MStateText.Size = new System.Drawing.Size(100, 20);
            this.MStateText.TabIndex = 6;
            this.MStateText.Text = "Состояние";
            // 
            // ConsoList
            // 
            this.ConsoList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ConsoList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.ConsoList.FormattingEnabled = true;
            this.ConsoList.Location = new System.Drawing.Point(1, 349);
            this.ConsoList.Name = "ConsoList";
            this.ConsoList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.ConsoList.Size = new System.Drawing.Size(128, 56);
            this.ConsoList.TabIndex = 7;
            this.ConsoList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ConsoList_KeyDown);
            // 
            // AddConsolidations
            // 
            this.AddConsolidations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AddConsolidations.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddConsolidations.Location = new System.Drawing.Point(1, 405);
            this.AddConsolidations.Name = "AddConsolidations";
            this.AddConsolidations.Size = new System.Drawing.Size(171, 23);
            this.AddConsolidations.TabIndex = 6;
            this.AddConsolidations.Text = "Добавить консолидации";
            this.AddConsolidations.UseVisualStyleBackColor = true;
            this.AddConsolidations.Click += new System.EventHandler(this.AddConsolidations_Click);
            // 
            // ButtonsPanel
            // 
            this.ButtonsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ButtonsPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ButtonsPanel.Location = new System.Drawing.Point(173, 405);
            this.ButtonsPanel.Name = "ButtonsPanel";
            this.ButtonsPanel.Size = new System.Drawing.Size(74, 23);
            this.ButtonsPanel.TabIndex = 6;
            this.ButtonsPanel.Text = "Кнопки";
            this.ButtonsPanel.UseVisualStyleBackColor = true;
            this.ButtonsPanel.Click += new System.EventHandler(this.ButtonsPanel_Click);
            // 
            // ConsolidationCount
            // 
            this.ConsolidationCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ConsolidationCount.AutoSize = true;
            this.ConsolidationCount.BackColor = System.Drawing.SystemColors.Control;
            this.ConsolidationCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ConsolidationCount.Location = new System.Drawing.Point(135, 349);
            this.ConsolidationCount.Name = "ConsolidationCount";
            this.ConsolidationCount.Size = new System.Drawing.Size(23, 25);
            this.ConsolidationCount.TabIndex = 10;
            this.ConsolidationCount.Text = "0";
            // 
            // CreateConsolidation
            // 
            this.CreateConsolidation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CreateConsolidation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CreateConsolidation.Location = new System.Drawing.Point(164, 349);
            this.CreateConsolidation.Name = "CreateConsolidation";
            this.CreateConsolidation.Size = new System.Drawing.Size(22, 23);
            this.CreateConsolidation.TabIndex = 11;
            this.CreateConsolidation.Text = "К";
            this.CreateConsolidation.UseVisualStyleBackColor = true;
            this.CreateConsolidation.Click += new System.EventHandler(this.CreateConsolidation_Click);
            // 
            // CreatePackage
            // 
            this.CreatePackage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CreatePackage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CreatePackage.Location = new System.Drawing.Point(192, 349);
            this.CreatePackage.Name = "CreatePackage";
            this.CreatePackage.Size = new System.Drawing.Size(22, 23);
            this.CreatePackage.TabIndex = 12;
            this.CreatePackage.Text = "М";
            this.CreatePackage.UseVisualStyleBackColor = true;
            this.CreatePackage.Click += new System.EventHandler(this.CreatePackage_Click);
            // 
            // CreatePallet
            // 
            this.CreatePallet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CreatePallet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CreatePallet.Location = new System.Drawing.Point(220, 349);
            this.CreatePallet.Name = "CreatePallet";
            this.CreatePallet.Size = new System.Drawing.Size(22, 23);
            this.CreatePallet.TabIndex = 13;
            this.CreatePallet.Text = "П";
            this.CreatePallet.UseVisualStyleBackColor = true;
            // 
            // TextWindowButton
            // 
            this.TextWindowButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TextWindowButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TextWindowButton.Location = new System.Drawing.Point(133, 376);
            this.TextWindowButton.Name = "TextWindowButton";
            this.TextWindowButton.Size = new System.Drawing.Size(53, 23);
            this.TextWindowButton.TabIndex = 14;
            this.TextWindowButton.Text = "Текст";
            this.TextWindowButton.UseVisualStyleBackColor = true;
            this.TextWindowButton.Click += new System.EventHandler(this.TextWindowButton_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.Location = new System.Drawing.Point(230, 376);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(24, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "Г";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // GTMField
            // 
            this.GTMField.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.GTMField.Location = new System.Drawing.Point(6, 64);
            this.GTMField.Name = "GTMField";
            this.GTMField.Size = new System.Drawing.Size(157, 20);
            this.GTMField.TabIndex = 17;
            this.GTMField.TextChanged += new System.EventHandler(this.GTMField_TextChanged);
            // 
            // SealField
            // 
            this.SealField.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.SealField.Location = new System.Drawing.Point(189, 64);
            this.SealField.Name = "SealField";
            this.SealField.Size = new System.Drawing.Size(62, 20);
            this.SealField.TabIndex = 18;
            this.SealField.TextChanged += new System.EventHandler(this.SealField_TextChanged);
            // 
            // AutoOpenGMX
            // 
            this.AutoOpenGMX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AutoOpenGMX.Location = new System.Drawing.Point(164, 64);
            this.AutoOpenGMX.Name = "AutoOpenGMX";
            this.AutoOpenGMX.Size = new System.Drawing.Size(22, 23);
            this.AutoOpenGMX.TabIndex = 19;
            this.AutoOpenGMX.Text = "А";
            this.AutoOpenGMX.UseVisualStyleBackColor = true;
            this.AutoOpenGMX.Click += new System.EventHandler(this.AutoOpenGMX_Click);
            // 
            // OpenCVToggle
            // 
            this.OpenCVToggle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OpenCVToggle.AutoSize = true;
            this.OpenCVToggle.Checked = true;
            this.OpenCVToggle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.OpenCVToggle.Location = new System.Drawing.Point(1, 238);
            this.OpenCVToggle.Name = "OpenCVToggle";
            this.OpenCVToggle.Size = new System.Drawing.Size(66, 17);
            this.OpenCVToggle.TabIndex = 21;
            this.OpenCVToggle.Text = "OpenCV";
            this.OpenCVToggle.UseVisualStyleBackColor = true;
            this.OpenCVToggle.CheckedChanged += new System.EventHandler(this.OpenCVToggle_CheckedChanged);
            // 
            // SetSealButton
            // 
            this.SetSealButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SetSealButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SetSealButton.Location = new System.Drawing.Point(227, 27);
            this.SetSealButton.Name = "SetSealButton";
            this.SetSealButton.Size = new System.Drawing.Size(22, 23);
            this.SetSealButton.TabIndex = 21;
            this.SetSealButton.Text = "S";
            this.SetSealButton.UseVisualStyleBackColor = true;
            this.SetSealButton.Click += new System.EventHandler(this.SetSealButton_Click);
            // 
            // StandartMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(255, 483);
            this.Controls.Add(this.OpenCVToggle);
            this.Controls.Add(this.AutoOpenGMX);
            this.Controls.Add(this.SealField);
            this.Controls.Add(this.GTMField);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.TextWindowButton);
            this.Controls.Add(this.CreatePallet);
            this.Controls.Add(this.CreatePackage);
            this.Controls.Add(this.CreateConsolidation);
            this.Controls.Add(this.ConsolidationCount);
            this.Controls.Add(this.ButtonsPanel);
            this.Controls.Add(this.AddConsolidations);
            this.Controls.Add(this.ConsoList);
            this.Controls.Add(this.MStateText);
            this.Controls.Add(this.SelectionCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.HistoryInvoice);
            this.Controls.Add(this.EnterInvoice);
            this.Name = "StandartMode";
            this.Text = "StandartMode";
            this.Load += new System.EventHandler(this.StandartMode_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.containerIDUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox EnterInvoice;
        private System.Windows.Forms.ListBox HistoryInvoice;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button AutoAddButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label StateText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox Reaction;
        private System.Windows.Forms.Button SetPortButton;
        private System.Windows.Forms.MaskedTextBox comText;
        private System.Windows.Forms.CheckBox EmulateCheckBox;
        private System.Windows.Forms.Label InvoceCountText;
        private System.Windows.Forms.Label SelectionCount;
        private System.Windows.Forms.Label MStateText;
        private System.Windows.Forms.CheckBox AutoMark;
        public System.Windows.Forms.ListBox ConsoList;
        private System.Windows.Forms.Button AddConsolidations;
        private System.Windows.Forms.Button ButtonsPanel;
        private System.Windows.Forms.Label ConsolidationCount;
        private System.Windows.Forms.ComboBox ContainerComboBox;
        private System.Windows.Forms.Button CreateConsolidation;
        private System.Windows.Forms.Button CreatePackage;
        private System.Windows.Forms.Button CreatePallet;
        private System.Windows.Forms.NumericUpDown containerIDUpDown;
        private System.Windows.Forms.Button TextWindowButton;
        private System.Windows.Forms.CheckBox OldMode;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.MaskedTextBox GTMField;
        private System.Windows.Forms.MaskedTextBox SealField;
        private System.Windows.Forms.Button AutoOpenGMX;
        private System.Windows.Forms.Button PrintMeshok;
        private System.Windows.Forms.CheckBox OpenCVToggle;
        private System.Windows.Forms.Button SetSealButton;
    }
}