// GonderiSurecForm.Designer.cs
namespace kargotakipsistemi.Forms
{
    partial class GonderiSurecForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // GonderiSurecForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Name = "GonderiSurecForm";
            Text = "GonderiSurecForm";
            Load += GonderiSurecForm_Load;
            ResumeLayout(false);
        }
    }
}