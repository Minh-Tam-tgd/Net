using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QUANLYTUYENSINH
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public readonly Entities db;
		public MainWindow()
		{
			InitializeComponent();
			db = new Entities();
			loadNganhHoc();
			loadTS();
		}

		private void btnThemTS_Click(object sender, RoutedEventArgs e)
		{
			var nganhhoc = cbx_NganhHoc.SelectedItem as NGANHHOC;
			if (nganhhoc == null)
			{
				MessageBox.Show("Vui lòng chọn ngành!");
				return;
			}
			THISINH addTS = new THISINH();
			try
			{
			addTS.MaTS = txtMaTS.Text;
			addTS.HoTen = txtHoTen.Text;
			addTS.NgaySinh = dpDate.SelectedDate.Value;
			addTS.DiemThi = double.Parse(txtDiemThi.Text);
			addTS.MaNganh = nganhhoc.MaNganh;
			db.THISINHs.Add(addTS);
			db.SaveChanges();
			MessageBox.Show("Thêm thành công!");
			loadTS();
			}
			catch (Exception ex)
			{
				MessageBox.Show("trùng mã,hoặc nhập sai dữ liệu! ");
			}


		}
		public void loadNganhHoc() {


			cbx_NganhHoc.ItemsSource = db.NGANHHOCs.ToList();
			cbx_NganhHoc.DisplayMemberPath = "TenNganh";
		}


		public void loadTS() {
			dtg_DS.ItemsSource = db.THISINHs.ToList();
			
	}


		private void dtg_DS_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if(dtg_DS.SelectedItem is THISINH ts){
			txtMaTS.Text = ts.MaTS;
			txtHoTen.Text = ts.HoTen;
				txtDiemThi.Text = ts.DiemThi.ToString();
				cbx_NganhHoc.SelectedItem = ts.NGANHHOC;
				dpDate.SelectedDate = ts.NgaySinh;

			}
		}

		private void btnXoa_Click(object sender, RoutedEventArgs e)
		{
			THISINH xoats=db.THISINHs.FirstOrDefault(x=>x.MaTS==txtMaTS.Text.Trim().ToUpper());
			if(xoats==null){
				MessageBox.Show("Không tìm thấy thí sinh");
				return;
			}
			db.THISINHs.Remove(xoats);
			db.SaveChanges();
			MessageBox.Show("Xóa thành công!");
			loadTS();

		}

		private void btnCapNhat_Click(object sender, RoutedEventArgs e)
		{
			var nganhhoc = cbx_NganhHoc.SelectedItem as NGANHHOC;
			THISINH suaTS = db.THISINHs.FirstOrDefault(x => x.MaTS == txtMaTS.Text.Trim().ToUpper());
			if (suaTS == null)
			{
				MessageBox.Show("Không tìm thấy thí sinh");
				return;
			}
			suaTS.MaTS = txtMaTS.Text;
			suaTS.HoTen = txtHoTen.Text;
			suaTS.NgaySinh= dpDate.SelectedDate.Value;
			suaTS.DiemThi = double.Parse(txtDiemThi.Text);
			suaTS.MaNganh = nganhhoc.MaNganh;
			db.SaveChanges();
			MessageBox.Show("Cập nhật thành công!");
			loadTS();
		}

		private void btn_Search_Click(object sender, RoutedEventArgs e)
		{
			String tim=txtSearch.Text.Trim().ToLower();
			var timts = db.THISINHs.Where(x => x.HoTen.ToLower().Contains(tim)).ToList();
			dtg_DS.ItemsSource = timts;

		}
	}
}
