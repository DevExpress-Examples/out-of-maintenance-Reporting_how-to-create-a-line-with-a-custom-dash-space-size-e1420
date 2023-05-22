using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;
using System.Drawing;
using DevExpress.Drawing;

namespace CustomXRLine {
    public class CustomLine : XRLine {
        int dash;
        int space;
        public CustomLine() : base() {
            this.dash = 1;
            this.space = 0;
        }
        [DisplayName("Dash size"), Browsable(true), DefaultValue(1)]
        public int Dash {
            get { return this.dash; }
            set { this.dash = value; }
        }
        [DisplayName("Space size"), Browsable(true), DefaultValue(0)]
        public int Space {
            get { return this.space; }
            set { this.space = value; }
        }
        protected override VisualBrick CreateBrick(VisualBrick[] childrenBricks) {
            if (this.LineStyle != DXDashStyle.Custom) {
                return base.CreateBrick(childrenBricks);
            }
            else {
                return new PanelBrick(this);
            }
        }

        protected override void PutStateToBrick(VisualBrick brick, PrintingSystemBase ps) {
            if (this.LineStyle != DXDashStyle.Custom) {
                base.PutStateToBrick(brick, ps);
            }
            else {
                PanelBrick panel = (PanelBrick)brick;
                panel.BackColor = Color.Transparent;
                panel.Sides = BorderSide.None;
                int i;
                if (this.LineDirection == LineDirection.Horizontal) {
                    for (i = 0; i < Convert.ToInt32(panel.Rect.Width / (this.Dash + this.Space)); i++) {
                        LineBrick dashBrick = new LineBrick();
                        dashBrick.Sides = BorderSide.None;
                        dashBrick.BackColor = panel.Style.ForeColor;
                        dashBrick.Rect = new RectangleF(i * (this.Dash + this.Space), panel.Rect.Height / 2 - this.LineWidth / 2, this.dash, this.LineWidth);
                        panel.Bricks.Add(dashBrick);
                    }
                    LineBrick endBrick = new LineBrick();
                    endBrick.Sides = BorderSide.None;
                    endBrick.BackColor = panel.Style.ForeColor;
                    endBrick.Rect = new RectangleF(i * (this.Dash + this.Space), panel.Rect.Height / 2 - this.LineWidth / 2, panel.Rect.Width - i * (this.Dash + this.Space), this.LineWidth);
                    panel.Bricks.Add(endBrick);
                }
                else if (this.LineDirection == LineDirection.Vertical) {
                    for (i = 0; i < Convert.ToInt32(panel.Rect.Height / (this.Dash + this.Space)); i++) {
                        LineBrick dashBrick = new LineBrick();
                        dashBrick.Sides = BorderSide.None;
                        dashBrick.BackColor = panel.Style.ForeColor;
                        dashBrick.Rect = new RectangleF(panel.Rect.Width / 2 - this.LineWidth / 2, i * (this.Dash + this.Space), this.LineWidth, this.Dash);
                        panel.Bricks.Add(dashBrick);
                    }
                    LineBrick endBrick = new LineBrick();
                    endBrick.Sides = BorderSide.None;
                    endBrick.BackColor = panel.Style.ForeColor;
                    endBrick.Rect = new RectangleF(panel.Rect.Width / 2 - this.LineWidth / 2, i * (this.Dash + this.Space), this.LineWidth, panel.Rect.Height - i * (this.Dash + this.Space));
                    panel.Bricks.Add(endBrick);
                }
            }
        }
    }
}
