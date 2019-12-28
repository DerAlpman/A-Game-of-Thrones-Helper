using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace AGOT
{
    public static class FlowDocumentConverter
    {
        public static FlowDocument NewFlowDocument(bool IsHyphenationEnabled, bool IsOptimalParagraphEnabled)
        {
            FlowDocument fd = new FlowDocument();
            fd.IsHyphenationEnabled = true;
            fd.IsOptimalParagraphEnabled = true;

            return fd;
        }
        public static FlowDocument AsFlowDocument(AttackOption ao)
        {
            FlowDocument fd = NewFlowDocument(true, true);

            Paragraph name = new Paragraph();
            name.FontWeight = FontWeights.Bold;
            name.Inlines.Add(ao.Name);
            fd.Blocks.Add(name);

            Regex rxExample = new Regex("Example");
            string[] delims = {"<p>"};
            foreach (string part in ao.Description.Split(delims, StringSplitOptions.RemoveEmptyEntries))
            {
                if (part.Trim().Length == 0)
                    continue;

                Paragraph p = new Paragraph();
                if (rxExample.IsMatch(part))
                {
                    p.FontStyle = FontStyles.Italic;
                    p.Inlines.Add(part);
                }
                else
                {
                    p.Inlines.Add(part);
                }
                fd.Blocks.Add(p);
            }

            return fd;
        }

        public static FlowDocument AsFlowDocument(RulesTable rt)
        {
            FlowDocument fd = NewFlowDocument(false, false);
            Paragraph titel = new Paragraph();
            titel.FontWeight = FontWeights.Bold;
            titel.Inlines.Add(rt.Name);
            fd.Blocks.Add(titel);

            // Create the Table...
            Table table = new Table();
            table.BorderBrush = new SolidColorBrush(Colors.Black);
            table.BorderThickness = new Thickness(1.0) ;
            // ...and add it to the FlowDocument Blocks collection.
            fd.Blocks.Add(table);

            // Set some global formatting properties for the table.
            
            int numberOfColumns = rt.Headers.Count();
            for (int x = 0; x < numberOfColumns; x++)
            {
                table.Columns.Add(new TableColumn());
            }
            // Create and add an empty TableRowGroup to hold the headers.
            table.RowGroups.Add(new TableRowGroup());
            table.RowGroups[0].Rows.Add(new TableRow());
            for (int h = 0; h < rt.Headers.Count(); h++)
            {
                TableCell headerCell = new TableCell(new Paragraph(new Run(rt.Headers[h])));
                /*headerCell.BorderBrush = new SolidColorBrush(Colors.Black);
                headerCell.BorderThickness = new Thickness(1.0);*/
                table.RowGroups[0].Rows[0].Cells.Add(headerCell);
            }
            table.RowGroups[0].FontSize = 12;

            foreach (string row in rt.Rows)
            {
                TableRow currentRow = new TableRow();
                table.RowGroups[0].Rows.Add(currentRow);
                foreach (string cell in row.Split('|'))
                {
                    TableCell rowCell = new TableCell(new Paragraph(new Run(cell)));
                    /*rowCell.BorderBrush = new SolidColorBrush(Colors.Black);
                    rowCell.BorderThickness = new Thickness(1.0);
                    rowCell.Padding = new Thickness(0);*/
                    currentRow.Cells.Add(rowCell);
                }
            }
            for (int x = 1; x < table.RowGroups[0].Rows.Count(); x++)
            {
                // Set alternating background colors for the middle colums.
                if (x % 2 == 0)
                    table.RowGroups[0].Rows[x].Background = Brushes.Beige;
                else
                    table.RowGroups[0].Rows[x].Background = Brushes.LightSteelBlue;
            }
            foreach(string at in rt.AdditionalText)
            {
                Paragraph t = new Paragraph();
                t.FontSize = 10;
                t.Inlines.Add(at);
                fd.Blocks.Add(t);
            }

            return fd;
        }

        public static FlowDocument AsFlowDocument(ClassFeature cf)
        {
            FlowDocument fd = NewFlowDocument(true, true);

            Paragraph name = new Paragraph();
            name.FontWeight = FontWeights.Bold;
            name.Inlines.Add(cf.Name);
            fd.Blocks.Add(name);

            string[] delims = { "<p>" };
            foreach (string part in cf.Description.Split(delims, StringSplitOptions.RemoveEmptyEntries))
            {
                if (part.Trim().Length == 0)
                    continue;

                Paragraph p = new Paragraph();
                p.Inlines.Add(part);
                fd.Blocks.Add(p);
            }
            return fd;
        }
    }
}
