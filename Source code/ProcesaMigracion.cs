using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Text;

namespace FBSMigracionBlazor
{

    public class ProcesaMigracion
	{
		public string FullName { get; set; }
		public string RootName { get; set; }

		public string Namespace { get; set; }
        public List<EstructuraMigracion> estructuras { get; set; }
        public Assembly Ensamblado { get; set; }
        public ProcesaMigracion()
		{
			estructuras = new List<EstructuraMigracion>();
		} 
	
		public void Procesa(Control rootControl,  string path)
		{
			estructuras = new List<EstructuraMigracion>();
			string tempFileName;
			System.IO.StreamWriter streamWriter;
			System.Text.StringBuilder stringBuilder;
			
			if(rootControl == null)
			{
				return;
			}

			this.Ensamblado = typeof(FBSMensajeria.A01_TipoCuentaMSE).Assembly;
			this.CheckNames(rootControl);
			stringBuilder = new System.Text.StringBuilder();
			this.ProcesaControles(rootControl);
			stringBuilder.AppendLine($"/{this.FullName} {Environment.NewLine}");

            if (estructuras.Any())
            {
				var salida =
							this.estructuras
								.OrderBy(x => x.Top)
								.Select(x => x.Componente)
								.Aggregate((x, y) => $"{x} {Environment.NewLine}{y}");

				stringBuilder.AppendLine(salida);
				stringBuilder.AppendLine(Environment.NewLine);
				var estructurasParaCode = estructuras
					.Where(x => !string.IsNullOrEmpty(x.Source));
				string paraCode=string.Empty;
				if (estructurasParaCode.Any())
                {
					paraCode= estructurasParaCode
						.Select(x => CrearPropiedad(x))
						.Aggregate((x, y) => $"{x} {Environment.NewLine}{y}");
                }


				stringBuilder.AppendLine($"@code {{ {Environment.NewLine}" + paraCode + $"{Environment.NewLine} }}");
			}		
		 

			tempFileName = path + string.Format($"\\{this.RootName}.razor");
			streamWriter = new System.IO.StreamWriter(tempFileName, false, System.Text.Encoding.Default);
			streamWriter.Write(stringBuilder.ToString());
			streamWriter.Flush();
			streamWriter.Close();			
		} 
		
		private void ProcesaControles(Control rootControl)
        {
			foreach (Control control in rootControl.Controls)
			{
				var estructura = new EstructuraMigracion();
				estructura.Top = control.Top;
				estructura.Left = control.Left;
				estructura.NombreControl = control.Name;			
				if (control is Label)
				{
					estructura.Tipo = "Label";
					estructura.Texto = control.Text;
					estructura.Componente = $"<FbsTexto Class=\"font-size-10 font-bold color-dark-blue\" TextoAMostrar=\"{estructura.Texto.ToUpperInvariant()}\"></FbsTexto>";
					if(!string.IsNullOrEmpty(estructura.Texto))
						estructuras.Add(estructura);
				}
				else if (control is TextBox)
				{
					var controlTextBox = (TextBox)control;					
					estructura.Tipo = Constantes.TIPO_TEXTO;
					if (control.DataBindings.Count > 0)
					{
						estructura.Propiedad = control.DataBindings[0].BindingMemberInfo.BindingMember;
						var dataSourceExterno = ((BindingSource)control.DataBindings[0].DataSource);
						dynamic dataSourceInterno = dataSourceExterno.DataSource;
						dynamic nombreTotal = dataSourceInterno.FullName;
						var type = this.Ensamblado.GetType(nombreTotal);						
						var propiedad = type.GetProperty(estructura.Propiedad);
						estructura.Source = propiedad.PropertyType.Name;
						estructura.ValorPropiedad = string.Concat(estructura.Propiedad[0].ToString().ToLower(), estructura.Propiedad.Substring(1));
					}
					estructura.Componente = $"<FbsInputText Tipo=\"{estructura.Source}\" ReadOnly=\"{controlTextBox.ReadOnly}\"  Valor=\"{estructura.ValorPropiedad}\" " +
						$"Nombre={estructura.NombreControl} ></FbsInputText>";
					estructuras.Add(estructura);

				}
				else if (control is ComboBox)
				{
					estructura.Tipo = Constantes.TIPO_COMBOBOX;
					string tipoPropiedadValor=string.Empty;
					var controlCombo = (ComboBox)control;
					dynamic dataSource = ((BindingSource)controlCombo.DataSource).DataSource;
					if(!string.IsNullOrEmpty(dataSource?.Name))
                    {
						estructura.Source = dataSource.Name;
						tipoPropiedadValor = this.Ensamblado.GetType(dataSource.FullName).GetProperty(controlCombo.ValueMember).PropertyType.Name;
						estructura.ValorPropiedad = string.Concat(estructura.Source[0].ToString().ToLower(), estructura.Source.Substring(1));
					}
					estructura.Componente = $"<FbsComboBox ListaValores=\"@{estructura.ValorPropiedad}\" TextoClass=\"font-size-10 font-regular\" " +
						$"PropiedadMostrar=\"{controlCombo.DisplayMember}\" PropiedadValor=\"{controlCombo.ValueMember}\" TElement=\"{estructura.Source}\" TItem=\"{tipoPropiedadValor}\" " +
						$"Valor=\"@valorSeleccionado\" ></FbsComboBox>";
					estructuras.Add(estructura);
				}
				else if (control is CheckBox)
                {
					estructura.Tipo = Constantes.TIPO_CHECKBOX;
					estructura.Source = "bool";
					estructura.ValorPropiedad = control.Name.Replace(estructura.Tipo, string.Empty);
					if (control.DataBindings.Count > 0)
                    {
						estructura.Propiedad = control.DataBindings[0].BindingMemberInfo.BindingMember;
					
					}
					var controlCheckBox = (CheckBox)control;
					estructura.Componente = $"<FbsCheckBox Texto=\"{controlCheckBox.Text.Trim()}\" Class=\"align-items-center\" ClassTexto=\"font-size-10 font-bold color-dark-blue mb-0\" " +
						$"OnCambioCheck = \"OnCambioCheck\" EstaSeleccionado = \"@{estructura.ValorPropiedad}\" PosicionTexto = \"@PosicionTextoCheckBox.DERECHA\" ></ FbsCheckBox>";
					estructuras.Add(estructura);
				}
				else if (control is BindingNavigator)
				{
					var controlBindingNavigator = (BindingNavigator)control;
					var estructurasMigracionBotones = ObtenerBotones(controlBindingNavigator.Items);
                    if (estructurasMigracionBotones.Any())
                    {
						estructuras.AddRange(estructurasMigracionBotones);
                    }
				}
				else if (control is GroupBox)
				{
					ProcesaControles(control);
				}
				else if(control is ToolStrip)
                {
					var toolStripControl = (ToolStrip)control;
					var estructurasMigracion=ObtenerBotones(toolStripControl.Items);
					if (estructurasMigracion.Any())
					{
						estructuras.AddRange(estructurasMigracion);
					}
				}
				else if (control is Panel)
                {
					var controlPanel = (Panel)control;
					ProcesaControles(controlPanel);
					 
                }
				else if (control is TableLayoutPanel)
				{
					ProcesaControles(control);
				}
				else if(control is DataGridView)
                {
					var controlDataGrid = (DataGridView)control;
					estructura=CrearTabla(controlDataGrid);
					estructuras.Add(estructura);					
                }
			} 
		} 

		private List<EstructuraMigracion> ObtenerBotones(ToolStripItemCollection controlesToolStrip)
        {
			var estructurasMigracionBotones = new List<EstructuraMigracion>();
			foreach(ToolStripItem control in controlesToolStrip)
			{
				string TipoIcono = string.Empty;
				var estructura = new EstructuraMigracion();
				estructura.Top = 0;
				estructura.Left = 0;
				estructura.Tipo = Constantes.TIPO_BOTON;
				estructura.NombreControl = control.Text.Trim();
                switch (estructura.NombreControl)
                {
                    case Constantes.GUARDAR:
						TipoIcono = "TipoIcono.GUARDAR";
						break;
					case Constantes.GUARDAR_CERRAR:
						TipoIcono = "TipoIcono.GUARDARCERRAR";
						break;
					case Constantes.GUARDAR_TODO:
						TipoIcono = "TipoIcono.GUARDARTODO";
						break;
					case Constantes.CANCELAR:
						TipoIcono = "TipoIcono.CANCELAR";
						break;
					case Constantes.INFORMACION:
						TipoIcono = "TipoIcono.INFORMACION";
						break;
					case Constantes.ACTUALIZAR:
						TipoIcono = "TipoIcono.ACTUALIZAR";
						break;
					case Constantes.IMPRIMIR:
						TipoIcono = "TipoIcono.IMPRIMIR";
						break;
					case Constantes.ELIMINAR:
						TipoIcono = "TipoIcono.ELIMINAR";
						break;
					case Constantes.SALIR:
						TipoIcono = "TipoIcono.SALIR";
						break;
						default:
						estructura.NombreControl = string.Empty;
						break;


				}

                if (!string.IsNullOrEmpty(estructura.NombreControl))
                {
					estructura.Componente = $"<FbsBoton UsarIcono=\"true\" IconoFbs=\"{TipoIcono}\" TipoBoton=\"FbsButtonType.TEXTO\" FormaBoton=\"FbsButtonShape.CIRCULO\" " +
						"TamanioBoton=\"FbsButtonSize.PEQUENIO\" Class=\"ml - 1\" OnClick = \"@(async (args, value) => await onClickMethod)\" />";
					estructurasMigracionBotones.Add(estructura);
                }

			}
			return estructurasMigracionBotones;
		}

		private EstructuraMigracion CrearTabla(DataGridView controlDataGrid)
        {
			var estructura =new EstructuraMigracion();
			
			estructura.Top=controlDataGrid.Top;
			estructura.Left=controlDataGrid.Left;
			estructura.Tipo = Constantes.TIPO_DATAGRID;
			estructura.ValorPropiedad = controlDataGrid.Name.Replace(estructura.Tipo, "Lista");
			var bindingDataSource = (BindingSource)controlDataGrid.DataSource;
			var dataSource=bindingDataSource.DataSource.ToString();
			estructura.Source = dataSource.Substring(dataSource.IndexOf('.') + 1);
			var columns = controlDataGrid.Columns;
			var listDataGridViewColumn = columns.Cast<DataGridViewColumn>().ToList();
			var headerList = listDataGridViewColumn.
				OrderBy(x => x.Index)
				.Select(x => x.HeaderText).ToList();
			var tablaStringBuilder = new StringBuilder();
			tablaStringBuilder.Append($"{ Environment.NewLine}<Table TItem=\"{estructura.Source}\" DataSource=\"@{estructura.ValorPropiedad}\" OnRowClick=\"OnRowClick\"> {Environment.NewLine}");
			var columnasCreadas = headerList
				.Select(x => TextoColumna(x))
				.Aggregate((x, y) => $"{x} {Environment.NewLine}{y}");
			tablaStringBuilder.AppendLine(columnasCreadas + Environment.NewLine + "</Table>");
			estructura.Componente = tablaStringBuilder.ToString();
			return estructura;
		}

		Func<EstructuraMigracion, string> CrearPropiedad = (x) =>
		{

			if (x.Tipo == Constantes.TIPO_COMBOBOX || x.Tipo==Constantes.TIPO_DATAGRID)
				return $" public List<{x.Source}> {x.ValorPropiedad} {{get; set;}}";
            else
				return $" public {x.Source} {x.ValorPropiedad} {{get; set;}}";
		};
	
		public void CheckNames(Control rootControl)
		{			
			if(this.Namespace == null)
			{
				this.Namespace = rootControl.GetType().Namespace;
			}
			this.RootName = rootControl.GetType().Name;
			this.FullName = rootControl.GetType().FullName;
		}

		public string TextoColumna(string x) => $"   <Column @bind-Field=\"context.{x.Replace(" ", string.Empty)}\"></Column>";

	}
		
		
} 