using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using IvyTalk.Printer.Label.Controls;
using IvyTalk.Printer.Label.Helper;

namespace IvyTalk.Printer.Label
{
    public class PrintD : Form, IBLL.IPrint, IDesign 
    {
        private System.Data.DataTable tbmain;
        private Panel panel2;
        private ContextMenuStrip contextMenuStrip1;
        private System.ComponentModel.IContainer components;
        private Panel pnl;
        private ContextMenuStrip contextMenuStrip2;
        private System.Data.DataTable tbdetail;

        private OperRecord operRecord;
        private IDesign des;
        private string path;
        public PrintD(string path)
        {
            this.InitializeComponent();
            des = this;
            this.path = path;
        }

        private string sheetType;
        void IBLL.IPrint.Print(DataTable dtMain, DataTable dtDetail, string sheetType)
        {
            try
            {
                tbmain = dtMain;
                tbdetail = dtDetail;
                this.sheetType = sheetType;
                //
                string file = path + "\\print_style\\" + sheetType + ".xml";
                if (System.IO.File.Exists(file) == true)
                {
                    string xml = System.IO.File.ReadAllText(path + "\\print_style\\" + sheetType + ".xml",
                    Encoding.GetEncoding("gb2312"));
                    IDesign des = this;
                    des.xml = xml;
                }
                else
                {
                    pnl.Width = (int)(21 * Conv.getAnCMInterval());
                    pnl.Height = (int)(29.7 * Conv.getAnCMInterval());
                }
                if (1 == 1)
                {
                    IDesign des = this;
                    operRecord = new OperRecord(des.xml);
                }
                //
                this.ShowDialog();

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            
        }

        public void Record()
        {
            OperRecord oper = new OperRecord(des.xml);
            operRecord.Next = oper;
            oper.Pre = operRecord;
            operRecord = oper;
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnl = new System.Windows.Forms.Panel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.pnl);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(731, 494);
            this.panel2.TabIndex = 1;
            // 
            // pnl
            // 
            this.pnl.BackColor = System.Drawing.Color.White;
            this.pnl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl.Location = new System.Drawing.Point(0, 0);
            this.pnl.Name = "pnl";
            this.pnl.Size = new System.Drawing.Size(545, 431);
            this.pnl.TabIndex = 0;
            this.pnl.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_Paint);
            this.pnl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnl_MouseClick);
            this.pnl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnl_MouseDown);
            this.pnl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnl_MouseMove);
            this.pnl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnl_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Name = "contextMenuStrip1";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            this.contextMenuStrip2.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip2_ItemClicked);
            // 
            // PrintD
            // 
            this.ClientSize = new System.Drawing.Size(731, 494);
            this.Controls.Add(this.panel2);
            this.KeyPreview = true;
            this.Name = "PrintD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "打印格式设计";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PrintD_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PrintD_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PrintD_KeyPress);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private System.Drawing.Point p = new System.Drawing.Point(0, 0);
        private System.Drawing.Point p2 = new System.Drawing.Point(0, 0);
        private void pnl_MouseClick(object sender, MouseEventArgs e)
        {
           
           
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            this.contextMenuStrip1.Visible = false;
            try
            {
                if (e.ClickedItem.Text == "保存")
                {
                    IDesign des = this;
                    string xml = des.xml;
                    //
                    string t = sheetType;
                    string file = path + "\\print_style\\" + t + ".xml";
                    System.IO.File.WriteAllText(file, xml, Encoding.GetEncoding("gb2312"));
                }
                else if (e.ClickedItem.Text == "页面")
                {
                    IInputSize ins = new InputSizeForPage();
                    System.Drawing.Size size = pnl.Size;
                    if (ins.Input(size, out size) == true)
                    {
                        pnl.Size = size;
                        Record();
                    }
                }
                else if (e.ClickedItem.Text == "背景")
                {
                    OpenFileDialog f = new OpenFileDialog();
                    f.Filter = "*.jpg|*.jpg";
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        pnl.BackgroundImage = Image.FromFile(f.FileName);
                        pnl.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                }
                else if (e.ClickedItem.Text == "撤销")
                {
                    if (operRecord.Pre == null)
                    {

                    }
                    else
                    {

                        operRecord.Pre.Undo(des);
                        operRecord = operRecord.Pre;
                    }
                }
                else if (e.ClickedItem.Text == "重做")
                {
                    if (operRecord.Next == null)
                    {

                    }
                    else
                    {

                        operRecord.Next.Undo(des);
                        operRecord = operRecord.Next;
                    }
                }
                else if (e.ClickedItem.Text == "文本")
                {
                    IPrintObject ins = new PrintObject1();
                    ins.SetSelectControl(this);
                    ISizeable sizeable = (ISizeable)ins;
                    IContextable contextable = (IContextable)ins;
                    sizeable.Location = this.p;
                    contextable.Context = "普通文本";
                    ins.Show(pnl);
                    Record();
                }
                else if (e.ClickedItem.Text == "表格")
                {
                    IPrintObject ins = new PrintObject3();
                    ins.SetSelectControl(this);
                    ISizeable sizeable = (ISizeable)ins;
                    sizeable.Location = this.p;
                    ins.Show(pnl);
                    Record();
                }
                else if (e.ClickedItem.Text == "竖线")
                {
                    IPrintObject ins = new PrintObject4();
                    ins.SetSelectControl(this);
                    ISizeable sizeable = (ISizeable)ins;
                    sizeable.Location = this.p;
                    ins.Show(pnl);
                    Record();
                }
                else if (e.ClickedItem.Text == "横线")
                {
                    IPrintObject ins = new PrintObject5();
                    ins.SetSelectControl(this);
                    ISizeable sizeable = (ISizeable)ins;
                    sizeable.Location = this.p;
                    ins.Show(pnl);
                    Record();
                }
                else if (e.ClickedItem.Text == "图片")
                {
                    IPrintObject ins = new PrintObject6();
                    ins.SetSelectControl(this);
                    ISizeable sizeable = (ISizeable)ins;
                    sizeable.Location = this.p;
                    ins.Show(pnl);
                    Record();
                }
                else if (e.ClickedItem.Text == "页码")
                {
                    IPrintObject ins = new PrintObject7();
                    ins.SetSelectControl(this);
                    ISizeable sizeable = (ISizeable)ins;
                    sizeable.Location = this.p;
                    ins.Show(pnl);
                    Record();
                }
                else if (e.ClickedItem.Text == "时间")
                {
                    IPrintObject ins = new PrintObject8();
                    ins.SetSelectControl(this);
                    ISizeable sizeable = (ISizeable)ins;
                    sizeable.Location = this.p;
                    ins.Show(pnl);
                    Record();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            
            

        }


        private void pnl_MouseDown(object sender, MouseEventArgs e)
        {
            
             this.p = new System.Drawing.Point(e.X, e.Y);
             this.p2 = new System.Drawing.Point(e.X, e.Y);
        }


        private Rectangle  rec = new Rectangle(0,0,0,0);

        private void pnl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.p2 = new System.Drawing.Point(e.X, e.Y);
                var rec2 = PrintObjectHelper.CreateRectangle(p, p2);
                ControlPaint.DrawReversibleFrame(rec, Color.White , FrameStyle.Dashed);
                rec = pnl.RectangleToScreen(rec2);
                ControlPaint.DrawReversibleFrame(rec, Color.White, FrameStyle.Dashed);
            }
        }

        private void menuFieldClick(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = (ToolStripMenuItem)sender;
            string field = menu.Text;
            IPrintObject ins = new PrintObject2();
            ins.SetSelectControl(this);
            ISizeable sizeable = (ISizeable)ins;
            sizeable.Location = this.p;
            IFieldAble fieldable = (IFieldAble)ins;
            fieldable.Field = field;
            ins.Show(pnl);
        }

        private List<IPrintObject> SelectObjects = new List<IPrintObject>();
        private void pnl_MouseUp(object sender, MouseEventArgs e)
        {

           
            //
            if (e.Button == MouseButtons.Right)
            {
                
                this.contextMenuStrip1.Items.Clear();
                this.contextMenuStrip1.Items.Add("保存");
                this.contextMenuStrip1.Items.Add("页面");
                this.contextMenuStrip1.Items.Add("背景");
                this.contextMenuStrip1.Items.Add("撤销");
                this.contextMenuStrip1.Items.Add("重做");
                this.contextMenuStrip1.Items.Add("-");
               

                ToolStripMenuItem menu = new ToolStripMenuItem();
                menu.Text = "数据";
                this.contextMenuStrip1.Items.Add(menu);
                foreach (System.Data.DataColumn col in tbmain.Columns)
                {
                    ToolStripMenuItem menu2 = new ToolStripMenuItem();
                    menu2.Text = col.ColumnName;
                    menu2.Click += this.menuFieldClick;
                    menu.DropDownItems.Add(menu2);
                }
                this.contextMenuStrip1.Items.Add("文本");
                this.contextMenuStrip1.Items.Add("表格");
                this.contextMenuStrip1.Items.Add("竖线");
                this.contextMenuStrip1.Items.Add("横线");
                this.contextMenuStrip1.Items.Add("图片");
                this.contextMenuStrip1.Items.Add("页码");
                this.contextMenuStrip1.Items.Add("时间");
                this.contextMenuStrip1.Tag = pnl;
                this.contextMenuStrip1.Show(pnl, e.X, e.Y);
            }
            else if (e.Button == MouseButtons.Left)
            {
                ControlPaint.DrawReversibleFrame(rec, Color.White, FrameStyle.Dashed);
                rec = new Rectangle(0, 0, 0, 0);
                //
                if (System.Windows.Forms.Control.ModifierKeys != Keys.Control)
                {
                    foreach (Control con in pnl.Controls)
                    {
                        IPrintObject ins = (IPrintObject)con;
                        ins.Selected = false;
                    }
                }
               
                var rec2 = PrintObjectHelper.CreateRectangle(p, p2);
                foreach (Control con in pnl.Controls)
                {
                    var rec3 = new Rectangle(con.Left, con.Top, con.Width, con.Height);
                    if (PrintObjectHelper.RectangleInRectangle(rec3, rec2) == true)
                    {
                        IPrintObject ins = (IPrintObject)con;
                        ins.Selected = true;

                    }
                }
                if (SelectObjects.Count == 0)
                {
                    _fistSelectObject = null;
                }
            }
        }



        void IDesign.Add(IPrintObject ins)
        {
            if (SelectObjects.Contains(ins) == false)
            {
                SelectObjects.Add(ins);
            }
        }

        void IDesign.Remove(IPrintObject ins)
        {
            if (SelectObjects.Contains(ins) == true)
            {
                SelectObjects.Remove(ins);
            }
            if (SelectObjects.Count == 0)
            {
                _fistSelectObject = null;
            }
        }



        void IDesign.RemoveAll()
        {
            foreach (Control con in pnl.Controls)
            {
                IPrintObject ins2 = (IPrintObject)con;
                ins2.Selected = false;
            }
            _fistSelectObject = null;
        }



        int IDesign.Count
        {
            get { return SelectObjects.Count; }
        }

        IPrintObject _fistSelectObject = null;
        IPrintObject IDesign.FirstSelectObject
        {
            get
            {
                return _fistSelectObject;
            }
            set
            {
                if (_fistSelectObject != null)
                {
                    _fistSelectObject.FirstSelected = false;
                }
                _fistSelectObject = value;
                if (_fistSelectObject != null)
                {
                    _fistSelectObject.FirstSelected = true;
                    var con = (Control)_fistSelectObject;
                    con.BringToFront();
                }
            }
        }


        void IDesign.OffSetX(int inte)
        {
         
            foreach (IPrintObject ins in SelectObjects)
            {
                ISizeable sizeable =(ISizeable) ins;
                sizeable.Location = new Point(sizeable.Location.X + inte, sizeable.Location.Y);
               
            }
            
        }

        void IDesign.OffSetY(int inte)
        {
           
            foreach (IPrintObject ins in SelectObjects)
            {
                ISizeable sizeable = (ISizeable)ins;
                sizeable.Location = new Point(sizeable.Location.X, sizeable.Location.Y + inte);
               
            }
           
        }

        void IDesign.OffSetWidth(int inte)
        {
            
            foreach (IPrintObject ins in SelectObjects)
            {
                ISizeable sizeable = (ISizeable)ins;
                sizeable.Size = new Size(sizeable.Size.Width+inte, sizeable.Size.Height);
                
            }
            
        }

        void IDesign.OffSetHeight(int inte)
        {
           
            foreach (IPrintObject ins in SelectObjects)
            {
                ISizeable sizeable = (ISizeable)ins;
                sizeable.Size = new Size(sizeable.Size.Width, sizeable.Size.Height + inte);
               
            }
            
        }

        private void PrintD_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.Shift==true)
            {
                if (e.KeyCode == Keys.Up)
                {
                    int flag = 0;
                    foreach (IPrintObject ins in SelectObjects)
                    {
                        ISizeable sizeable = (ISizeable)ins;
                        sizeable.Size = new Size(sizeable.Size.Width, sizeable.Size.Height - 1);
                        flag = 1;
                    }
                    if (flag == 1)
                    {
                        Record();
                    }
                }
                else if (e.KeyCode == Keys.Down)
                {
                    int flag = 0;
                    foreach (IPrintObject ins in SelectObjects)
                    {
                        ISizeable sizeable = (ISizeable)ins;
                        sizeable.Size = new Size(sizeable.Size.Width, sizeable.Size.Height + 1);
                        flag = 1;
                    }
                    if (flag == 1)
                    {
                        Record();
                    }
                }
                else if (e.KeyCode == Keys.Left)
                {
                    int flag = 0;
                    foreach (IPrintObject ins in SelectObjects)
                    {
                        ISizeable sizeable = (ISizeable)ins;
                        sizeable.Size = new Size(sizeable.Size.Width - 1, sizeable.Size.Height);
                        flag = 1;
                    }
                    if (flag == 1)
                    {
                        Record();
                    }
                }
                else if (e.KeyCode == Keys.Right)
                {
                    int flag = 0;
                    foreach (IPrintObject ins in SelectObjects)
                    {
                        ISizeable sizeable = (ISizeable)ins;
                        sizeable.Size = new Size(sizeable.Size.Width + 1, sizeable.Size.Height);
                        flag = 1;
                    }
                    if (flag == 1)
                    {
                        Record();
                    }
                }
                if (e.Control == true && e.KeyCode == Keys.Z)
                {
                    if (operRecord.Next == null)
                    {

                    }
                    else
                    {
                        operRecord.Next.Undo(des);
                        operRecord = operRecord.Next;
                    }
                }
            }
            else if (e.Control == true)
            {
                if (e.KeyCode == Keys.A)
                {
                    foreach (IPrintObject ins in pnl.Controls)
                    {
                        ins.Selected = true;
                    }
                }
                if (e.KeyCode == Keys.S)
                {
                    IDesign des = this;
                    string xml = des.xml;
                    //
                    string t = sheetType;
                    string file = path + "\\print_style\\" + t + ".xml";
                    System.IO.File.WriteAllText(file, xml, Encoding.GetEncoding("gb2312"));
                }
                if (e.KeyCode == Keys.Z)
                {
                 
                    if (operRecord.Pre == null)
                    {
                       
                    }
                    else
                    {
                        
                        operRecord.Pre.Undo(des);
                        operRecord = operRecord.Pre;
                    }
                }
            }
            else
            {

                if (e.KeyCode == Keys.Up)
                {
                    int flag = 0;
                    foreach (IPrintObject ins in SelectObjects)
                    {
                        ISizeable sizeable = (ISizeable)ins;
                        sizeable.Location = new Point(sizeable.Location.X, sizeable.Location.Y - 1);
                        flag = 1;
                    }
                    if (flag == 1)
                    {
                        Record();
                    }
                }
                else if (e.KeyCode == Keys.Down)
                {
                    int flag = 0;
                    foreach (IPrintObject ins in SelectObjects)
                    {
                        ISizeable sizeable = (ISizeable)ins;
                        sizeable.Location = new Point(sizeable.Location.X, sizeable.Location.Y + 1);
                        flag = 1;
                    }
                    if (flag == 1)
                    {
                        Record();
                    }
                }
                else if (e.KeyCode == Keys.Left)
                {
                    int flag = 0;
                    foreach (IPrintObject ins in SelectObjects)
                    {
                        ISizeable sizeable = (ISizeable)ins;
                        sizeable.Location = new Point(sizeable.Location.X - 1, sizeable.Location.Y);
                        flag = 1;
                    }
                    if (flag == 1)
                    {
                        Record();
                    }
                }
                else if (e.KeyCode == Keys.Right)
                {
                    int flag = 0;
                    foreach (IPrintObject ins in SelectObjects)
                    {
                        ISizeable sizeable = (ISizeable)ins;
                        sizeable.Location = new Point(sizeable.Location.X + 1, sizeable.Location.Y);
                        flag = 1;
                    }
                    if (flag == 1)
                    {
                        Record();
                    }
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    int flag = 0;
                    List<IPrintObject> lst = new List<IPrintObject>();
                    foreach (IPrintObject ins in SelectObjects)
                    {
                        lst.Add(ins);
                    }
                    foreach (IPrintObject ins in lst)
                    {
                        if (ins.GetType().GetInterface(typeof(IDeleteable).ToString()) != null)
                        {
                            IDeleteable del = (IDeleteable)ins;
                            del.Delete(pnl);
                            flag = 1;
                        }
                    }
                    if (flag == 1)
                    {
                        Record();
                    }
                }
            }
        }

        

        private void contextMenuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            this.contextMenuStrip2.Visible = false;
            try
            {
                if (e.ClickedItem.Text == "文本内容")
                {
                    IInput input = new InputString();
                    string def = "";
                    if (SelectObjects.Count == 1)
                    {
                        IContextable contextable = (IContextable)SelectObjects[0];
                        def = contextable.Context;
                    }
                    if (input.Input(def, out def) == true)
                    {
                        foreach (IPrintObject ins in SelectObjects)
                        {
                            IContextable contextable = (IContextable)ins;
                            contextable.Context = def;
                        }
                        Record();
                    }

                }
                else if (e.ClickedItem.Text == "文本对齐")
                {
                    ISelectContextAlign align = new SelectContextAlign();
                    int def = 0;
                    if (SelectObjects.Count == 1)
                    {
                        IContextAlignAble contextalignable = (IContextAlignAble)SelectObjects[0];
                        def = contextalignable.Align;
                    }
                    if (align.Select(def,out def)==true )
                    {
                        foreach (IPrintObject ins in SelectObjects)
                        {
                            IContextAlignAble contextalignable = (IContextAlignAble)ins;
                            contextalignable.Align = def;
                        }
                        Record();
                    }
                }
                else if (e.ClickedItem.Text == "区域")
                {
                    IInput input = new InputString();
                    int Area = -1;
                    if (SelectObjects.Count == 1)
                    {
                        IChangeAreaAble changeareaable = (IChangeAreaAble)SelectObjects[0];
                        Area = changeareaable.Area;
                    }
                    IChangePrintArea printarea = new ChangePrintArea();
                    if (printarea.Change(Area, out Area) == true)
                    {
                        foreach (IPrintObject ins in SelectObjects)
                        {
                            IChangeAreaAble changeareaable = (IChangeAreaAble)ins;
                           changeareaable.Area=Area;
                        }
                        Record();
                    }
                }
                else if (e.ClickedItem.Text == "删除")
                {
                    List<IPrintObject> lst = new List<IPrintObject>();
                    foreach (IPrintObject ins in SelectObjects)
                    {
                        lst.Add(ins);
                    }
                    foreach (IPrintObject ins in lst)
                    {
                        IDeleteable deleteable = (IDeleteable)ins;
                        deleteable.Delete(pnl);
                    }
                    Record();
                }
                else if (e.ClickedItem.Text == "字体")
                {
                    FontDialog f = new FontDialog();

                    if (SelectObjects.Count == 1)
                    {
                        IFontable fontable = (IFontable)SelectObjects[0];
                        f.Font = fontable.Font;
                    }
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        foreach (IPrintObject ins in SelectObjects)
                        {
                            IFontable fontable = (IFontable)ins;
                            fontable.Font = f.Font;
                        }
                        Record();
                    }
                }
                else if (e.ClickedItem.Text == "边框")
                {
                    IEditBorder border = new EditBorder();
                    int borderLeft = 0;
                        int borderRight=0;
                            int borderTop=0;
                    int borderBottom=0;
                    if (SelectObjects.Count == 1)
                    {
                        IBorderable borderable = (IBorderable)SelectObjects[0];
                        borderLeft = borderable.BorderLeft;
                        borderRight = borderable.BorderRight;
                        borderTop = borderable.BorderTop;
                        borderBottom = borderable.BorderBottom;
                    }
                    if (border.EditBorder(borderLeft, borderRight, borderTop, borderBottom,
                        out borderLeft, out borderRight, out borderTop, out borderBottom) == true)
                    {
                        foreach (IPrintObject ins in SelectObjects)
                        {
                            IBorderable borderable = (IBorderable)ins;
                              borderable.BorderLeft=borderLeft ;
                           borderable.BorderRight=borderRight ;
                          borderable.BorderTop=borderTop ;
                              borderable.BorderBottom=borderBottom;
                        }
                        Record();
                    }
                }
                else if (e.ClickedItem.Text == "颜色")
                {
                    ColorDialog f = new ColorDialog();
                    Color def = Color.Black;
                    if (SelectObjects.Count == 1)
                    {
                        IColorable colorable = (IColorable)SelectObjects[0];
                        def = colorable.Color;
                    }
                    f.Color = def;
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        foreach (IPrintObject ins in SelectObjects)
                        {
                            IColorable colorable = (IColorable)ins;
                            colorable.Color = f.Color;
                        }
                        Record();
                    }

                }
                else if (e.ClickedItem.Text == "左对齐")
                {
                    ISizeable sizeable2 = (ISizeable)_fistSelectObject;
                    foreach (IPrintObject ins in SelectObjects)
                    {
                        ISizeable sizeable = (ISizeable)ins;

                        sizeable.Location = new Point(sizeable2.Location.X, sizeable.Location.Y);
                    }
                    Record();
                }
                else if (e.ClickedItem.Text == "右对齐")
                {
                    ISizeable sizeable2 = (ISizeable)_fistSelectObject;
                    foreach (IPrintObject ins in SelectObjects)
                    {
                        ISizeable sizeable = (ISizeable)ins;

                        sizeable.Location = new Point((sizeable2.Location.X + sizeable2.Size.Width) - sizeable.Size.Width, sizeable.Location.Y);
                    }
                    Record();
                }
                else if (e.ClickedItem.Text == "上对齐")
                {
                    ISizeable sizeable2 = (ISizeable)_fistSelectObject;
                    foreach (IPrintObject ins in SelectObjects)
                    {
                        ISizeable sizeable = (ISizeable)ins;

                        sizeable.Location = new Point(sizeable.Location.X, sizeable2.Location.Y);
                    }
                    Record();
                }
                else if (e.ClickedItem.Text == "下对齐")
                {
                    ISizeable sizeable2 = (ISizeable)_fistSelectObject;
                    foreach (IPrintObject ins in SelectObjects)
                    {
                        ISizeable sizeable = (ISizeable)ins;

                        sizeable.Location = new Point(sizeable.Location.X, (sizeable2.Location.Y + sizeable2.Size.Height) - sizeable.Size.Height);
                    }
                    Record();
                }
                else if (e.ClickedItem.Text == "表格内容")
                {
                    IGridable gridable = (IGridable)_fistSelectObject;
                    List<string> lst = new List<string>();
                   
                    if (tbdetail != null)
                    {
                        foreach (System.Data.DataColumn col in tbdetail.Columns)
                        {
                            if (col.ColumnName.StartsWith("#") == false)
                            {
                                lst.Add(col.ColumnName);
                            }
                          
                        }
                    }

                    if (lst.Contains("#") == false)
                    {
                        lst.Add("#");
                    }
                    if (lst.Contains("i") == false)
                    {
                        lst.Add("i");
                    }
                    
                    
                   
                    if (gridable.EditGrid(lst.ToArray()) == true)
                    {
                        Record();
                    }

                }
                else if (e.ClickedItem.Text == "表格格式")
                {
                    IGridable gridable = (IGridable)_fistSelectObject;
                    if (gridable.SetStyle() == true)
                    {
                        Record();
                    }

                }
                else if (e.ClickedItem.Text == "导入图片")
                {
                    OpenFileDialog f = new OpenFileDialog();
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        var fileInfo = new System.IO.FileInfo(f.FileName);
                        decimal len = Conv.ToDecimal(fileInfo.Length);
                        len = len / 1024;
                        if (len > 500)
                        {
                            throw new Exception("图片文件大于500K");
                        }
                        var img = Image.FromFile(f.FileName);
                        IImageAble imageable = (IImageAble)_fistSelectObject;
                        imageable.Image = img;
                        Record();
                    }
                }
                else if (e.ClickedItem.Text == "导出图片")
                {
                    IImageAble imageable = (IImageAble)_fistSelectObject;
                    if (imageable.Image == null)
                    {
                        throw new Exception("无可导出的图片");
                    }
                    else
                    {
                        SaveFileDialog f = new SaveFileDialog();
                        f.Filter = "*.jpg|*.jpg";
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            imageable.Image.Save(f.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        }
                    }

                }
                else if (e.ClickedItem.Text == "属性")
                {
                    System.Windows.Forms.MessageBox.Show(_fistSelectObject.propertyInfo, "属性");
                }
                else if (e.ClickedItem.Text == "格式化")
                {
                    IInput input = new InputFormatString();
                    string def = "";
                    if (SelectObjects.Count == 1)
                    {
                        IFormatable formatable = (IFormatable)SelectObjects[0];
                        def = formatable.Format;
                    }
                    if (input.Input(def, out def) == true)
                    {
                        foreach (IPrintObject ins in SelectObjects)
                        {
                            IFormatable formatable = (IFormatable)ins;
                            formatable.Format = def;
                        }
                        Record();
                    }
                }
                else if (e.ClickedItem.Text == "改字段")
                {

                    string def = "";
                    if (SelectObjects.Count == 1)
                    {
                        IFieldAble fieldable = (IFieldAble)SelectObjects[0];
                        def = fieldable.Field;
                    }
                    IChangeField chg = new ChangeField();
                    List<string> lst = new List<string>();
                    if (tbmain != null)
                    {
                        foreach (System.Data.DataColumn col in tbmain.Columns)
                        {
                            lst.Add(col.ColumnName);
                        }
                    }
                    if (chg.Change(lst.ToArray(), def, out def) == true)
                    {
                        foreach (IPrintObject ins in SelectObjects)
                        {
                            IFieldAble fieldable = (IFieldAble)ins;
                            fieldable.Field = def;
                        }
                        Record();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            
        }



        void IDesign.ShowMenu(Point scrPoint)
        {
            var ts = PrintObjectHelper.GetTypes(SelectObjects.ToArray());
            this.contextMenuStrip2.Items.Clear();
            foreach (Type t in ts)
            {
                if (t == typeof(IChangeAreaAble))
                {
                    this.contextMenuStrip2.Items.Add("区域");
                }
                else if (t == typeof(IColorable))
                {
                    this.contextMenuStrip2.Items.Add("颜色");
                }
                else if (t == typeof(IContextable))
                {
                    this.contextMenuStrip2.Items.Add("文本内容");
                }
                else if (t == typeof(IContextAlignAble))
                {
                    this.contextMenuStrip2.Items.Add("文本对齐");
                }
                else if (t == typeof(IDeleteable))
                {
                    this.contextMenuStrip2.Items.Add("删除");
                }
                else if (t == typeof(IFontable))
                {
                    this.contextMenuStrip2.Items.Add("字体");
                }
                else if (t == typeof(IBorderable))
                {
                    this.contextMenuStrip2.Items.Add("边框");
                }
                else if (t == typeof(IFormatable))
                {
                    this.contextMenuStrip2.Items.Add("格式化");
                }
                else if (t == typeof(IImageAble))
                {
                    this.contextMenuStrip2.Items.Add("导入图片");
                    this.contextMenuStrip2.Items.Add("导出图片");
                }
                else if (t == typeof(IFieldAble))
                {
                    this.contextMenuStrip2.Items.Add("改字段");
                }
                else if (t == typeof(ISizeable))
                {
                    this.contextMenuStrip2.Items.Add("左对齐");
                    this.contextMenuStrip2.Items.Add("右对齐");
                    this.contextMenuStrip2.Items.Add("上对齐");
                    this.contextMenuStrip2.Items.Add("下对齐");
                }
                else if (t == typeof(IGridable))
                {
                    this.contextMenuStrip2.Items.Add("表格内容");
                    this.contextMenuStrip2.Items.Add("表格格式");
                }
                else if (t == typeof(IPrintObject))
                {
                    this.contextMenuStrip2.Items.Add("属性");
                }
            }
            //
           var p= pnl.PointToClient(scrPoint);
           this.contextMenuStrip2.Show(pnl, p.X, p.Y);
        }

        private void PrintD_Load(object sender, EventArgs e)
        {

        }

        private void PrintD_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            return false;
             
        }

        private void pnl_Paint(object sender, PaintEventArgs e)
        {
            StringFormat sf=new StringFormat();
            sf.Alignment=StringAlignment.Center;
            sf.LineAlignment=StringAlignment.Center;
            //e.Graphics.DrawString("右键添加项", pnl.Font, new SolidBrush(Color.Gray), new Rectangle(0, 0, pnl.Width, pnl.Height), sf);
        }

        string IDesign.xml
        {
            get
            {
                StringBuilderForXML sb = new StringBuilderForXML();
                string str = "";
                sb.Append("Width", pnl.Width.ToString());
                sb.Append("Height", pnl.Height.ToString());
                str = sb.ToString();
                sb.Clear();
                sb.Append("Page", str);
                //
                foreach (IPrintObject ins in pnl.Controls)
                {
                    sb.Append(ins.xml);
                }
                //
                str = sb.ToString();
                sb.Clear();
                sb.Append("xml", str);
                return sb.ToString();
            }
            set
            {
                List<IPrintObject> lst = new List<IPrintObject>();
                foreach (IPrintObject ins in pnl.Controls)
                {
                    lst.Add(ins);
                }
                foreach (IPrintObject ins in lst)
                {
                    ins.Selected = false;
                    if (ins.GetType().GetInterface(typeof(IDeleteable).ToString()) != null)
                    {
                        IDeleteable del = (IDeleteable)ins;
                        del.Delete(pnl);
                    }
                }
                 
                //
                ReadXml r = new ReadXml(value);
                int width, height;
                int.TryParse(r.Read("Page/Width"), out width);
                int.TryParse(r.Read("Page/Height"), out height);
                pnl.Width = width;
                pnl.Height = height;

                //
                foreach (ReadXml r2 in r.ReadList("PrintObject1"))
                {
                    IPrintObject ins = new PrintObject1();
                    ins.SetSelectControl(this);
                    ins.xml = r2.Context;
                    ins.Show(pnl);
                }
                //
                foreach (ReadXml r2 in r.ReadList("PrintObject2"))
                {
                    IPrintObject ins = new PrintObject2();
                    ins.SetSelectControl(this);
                    ins.xml = r2.Context;
                    ins.Show(pnl);
                }
                //
                foreach (ReadXml r2 in r.ReadList("PrintObject3"))
                {
                    IPrintObject ins = new PrintObject3();
                    ins.SetSelectControl(this);
                    ins.xml = r2.Context;
                    ins.Show(pnl);
                }
                //
                foreach (ReadXml r2 in r.ReadList("PrintObject4"))
                {
                    IPrintObject ins = new PrintObject4();
                    ins.SetSelectControl(this);
                    ins.xml = r2.Context;
                    ins.Show(pnl);
                }
                //
                foreach (ReadXml r2 in r.ReadList("PrintObject5"))
                {
                    IPrintObject ins = new PrintObject5();
                    ins.SetSelectControl(this);
                    ins.xml = r2.Context;
                    ins.Show(pnl);
                }
                //
                foreach (ReadXml r2 in r.ReadList("PrintObject6"))
                {
                    IPrintObject ins = new PrintObject6();
                    ins.SetSelectControl(this);
                    ins.xml = r2.Context;
                    ins.Show(pnl);
                }
                //
                foreach (ReadXml r2 in r.ReadList("PrintObject7"))
                {
                    IPrintObject ins = new PrintObject7();
                    ins.SetSelectControl(this);
                    ins.xml = r2.Context;
                    ins.Show(pnl);
                }
                //
                foreach (ReadXml r2 in r.ReadList("PrintObject8"))
                {
                    IPrintObject ins = new PrintObject8();
                    ins.SetSelectControl(this);
                    ins.xml = r2.Context;
                    ins.Show(pnl);
                }
                //
            }
        }
    }
}
