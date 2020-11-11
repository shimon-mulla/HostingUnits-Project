using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace BE
{
    public class TableDisplay
    {
        protected Dictionary<PropertiesData, ColumnDisplay> columns = new Dictionary<PropertiesData, ColumnDisplay>();
        private List<Enum> ToShow = new List<Enum>();
        private List<Enum> NotToShow = new List<Enum>();
        //private List<ColumnDisplay> columns = new List<ColumnDisplay>();
        protected List<int> listIndexesToDelete = new List<int>();
        private bool isDeleteAble = false;
        private bool isUpdateAble = false;
        private bool isViewable = false;

        public bool ToDelete { get { return isDeleteAble; } set { isDeleteAble = value; } }
        public bool ToUpdate { get { return isUpdateAble; } set { isUpdateAble = value; } }
        public bool ToView { get { return isViewable; } set { isViewable = value; } }

        /*public void setColumns(params ColumnDisplay[] cols)
        {
            //foreach(ColumnDisplay)
            columns.Clear();
            columns = cols.ToList();
        }
        */

        public void setColumns(Dictionary<PropertiesData, ColumnDisplay> cols)
        {
            columns.Clear();
            columns = cols;//(Dictionary<PropertiesData, ColumnDisplay>)cols;

        }

        public void setToShow(params Enum[] properties)
        {
            ToShow = properties.ToList();
        }

        public void setNotToShow(params Enum[] properties)
        {
            NotToShow = properties.ToList();
        }        

        public List<DataGridColumn> getGridColumns()
        {
            List<DataGridColumn> columnsList = new List<DataGridColumn>();


            /*CheckBox checkbox1 = new CheckBox
            {
                FlowDirection = FlowDirection.RightToLeft
            };
            */
            var buttonTemplate = new FrameworkElementFactory(typeof(CheckBox));
            buttonTemplate.SetBinding(Button.ContentProperty, new Binding("Name"));
            buttonTemplate.SetBinding(Button.ContentProperty, new Binding("TabIndex"));
            //buttonTemplate.tag = "mtan";
            //buttonTemplate.
            /*buttonTemplate.SetBinding(Button.IsEnabledProperty, new Binding("Status")
            {
                Converter = new StatusToEnabledConverter()
            });*/
            buttonTemplate.AddHandler(
                CheckBox.CheckedEvent,
                new RoutedEventHandler((o, e) => {
                    CheckBox checkbox = o as CheckBox;
                    MessageBox.Show(checkbox.Tag + "");
                    //(checkbox.Parent.GetValue.)
                })
            );

            buttonTemplate.AddHandler(
                CheckBox.UncheckedEvent,
                new RoutedEventHandler((o, e) => {
                }/*MessageBox.Show("Uncheck")*/)
            );


            CheckBox ch = new CheckBox
            {
                TabIndex = 0
            };
            //  https://stackoverflow.com/questions/38177490/loop-through-checkboxcolumn-in-datagrid-in-wpf-checked-or-not

            /*if (isDeleteAble)
                columnsList.Add(new DataGridCheckBoxColumn
                {
                      ElementStyle = new ContentElement
                })*/
                /*columnsList.Add(new DataGridTemplateColumn
                {
                    Header = "מחיקה",
                    CellTemplate = new DataTemplate() {    Template = ch },
                    
                  
                });*/
            



            //if(isUpdateAble)



            if (ToShow.Count > 0)
            {
                foreach(PropertiesData property in ToShow)
                {
                    if (NotToShow.Contains(property) || !columns.ContainsKey(property))
                        continue;

                    

                    DataGridTextColumn gridColumn = new DataGridTextColumn
                    {
                        Header = columns[property].Header,
                        Width = columns[property].Width,
                    };

                    if (columns[property].isBindNeeded)
                    {
                        gridColumn.Binding = new Binding { Path = new PropertyPath(columns[property].Path) };
                    }



                    columnsList.Add(gridColumn);
                }
            }
            else
            {
                foreach (var column in columns)
                {
                    if (NotToShow.Contains(column.Key))
                        continue;
                    

                    DataGridTextColumn gridColumn = new DataGridTextColumn
                    {
                        Header = column.Value.Header,
                        Width = column.Value.Width,
                    };

                    if (column.Value.isBindNeeded)
                    {
                        gridColumn.Binding = new Binding { Path = new PropertyPath(column.Value.Path) };
                    }



                    columnsList.Add(gridColumn);
                }
            }

            return columnsList;
        }


        /*
        public List<DataGridColumn>  getGridColumns()
        {
            List<DataGridColumn> columnsList = new List<DataGridColumn>();

            foreach(ColumnDisplay column in columns)
            {
                DataGridTextColumn gridColumn = new DataGridTextColumn {
                    Header = column.Header,
                    Width = column.Width,
                };

                if(column.isBindNeeded)
                {
                    gridColumn.Binding = new Binding {Path = new PropertyPath(column.Path) };
                }



                columnsList.Add(gridColumn);
            }

            return columnsList;
        }*/




    }
}
