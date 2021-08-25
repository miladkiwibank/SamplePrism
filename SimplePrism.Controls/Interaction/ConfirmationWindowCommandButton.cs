using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePrism.Controls.Interaction
{
    public class ConfirmationWindowCommandButton
    {
        public string CommandName { get; set; }

        public string DisplayName { get; set; }

        public string Color { get; set; }

        public string HoverColor { get; set; }

        public string Description { get; set; }

        public ConfirmationWindowCommandButton(string commandDefinition)
        {
            this.HoverColor = "Silver";
            this.Color = "Gainsboro";
            if (string.IsNullOrWhiteSpace(commandDefinition) || commandDefinition.Trim().StartsWith("=")
                || commandDefinition.Trim().StartsWith("#"))
            {
                return;
            }
            commandDefinition = commandDefinition.Trim(new char[]
            {
                '"'
            });
            this.SetDisplayName(commandDefinition);
            if (commandDefinition.Contains("="))
            {
                string[] array = commandDefinition.Split(new char[]
                {
                    '='
                }, 2);
                this.SetDisplayName(array[0]);
                this.CommandName = array[1];
            }
            else
            {
                this.CommandName = commandDefinition;
            }
            if (this.CommandName.Contains(":"))
            {
                string[] array2 = this.CommandName.Split(new char[]
                {
                    ':'
                }, 2);
                this.CommandName = array2[0];
                this.SetColor(array2[1]);
            }
        }

        private void SetDisplayName(string displayName)
        {
            this.DisplayName = displayName;
            if (displayName.Contains("#"))
            {
                string[] array = displayName.Split(new char[]
                {
                    '#'
                });
                this.DisplayName = array[0];
                this.Description = array[1];
                if (string.IsNullOrEmpty(this.Description))
                {
                    this.DisplayName = "";
                }
            }
        }

        private void SetColor(string color)
        {
            this.HoverColor = color;
            if (color.Contains(";"))
            {
                string[] array = color.Split(new char[]
                {
                    ';'
                }, 2);
                this.Color = array[0];
                this.HoverColor = array[1];
            }
        }
    }
}
