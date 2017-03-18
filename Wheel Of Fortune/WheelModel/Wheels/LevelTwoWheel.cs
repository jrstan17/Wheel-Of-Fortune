using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Wheel_Of_Fortune.Enums;

namespace Wheel_Of_Fortune.WheelModel.Wheels {
    class LevelTwoWheel : Wheel {
        public override void Initialize() {
            Spaces = new List<Space>();
            Space space = new Space();
            Wedge wedge = new Wedge();
            Third third = new Third();

            third.Text = "BANKRUPT";
            third.Value = 0;
            third.Type = ThirdType.Bankrupt;
            third.SetColor(WheelColors.WHITE, WheelColors.BLACK);
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            third.Text = "$950";
            third.Value = 950;
            third.Type = ThirdType.Regular;
            third.SetColor(WheelColors.BLACK, WheelColors.PINK);
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            third.Text = "$550";
            third.Value = 550;
            third.Type = ThirdType.Regular;
            third.SetColor(WheelColors.BLACK, WheelColors.TEAL);
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            ColorSet colorSetText = new ColorSet();
            ColorSet colorSetBack = new ColorSet();
            Space s = new Space();
            third.Text = "PRIZE";
            third.Value = 500;
            third.Type = ThirdType.Prize;
            colorSetText.TopColor = WheelColors.BLACK;
            colorSetText.BottomColor = Colors.White;
            colorSetBack.TopColor = Colors.LightBlue;
            colorSetBack.BottomColor = Colors.DarkSlateBlue;
            third.SetColor(colorSetText.DeepCopy(), colorSetBack.DeepCopy());
            wedge = new Wedge(third, 3);
            s.Add(wedge.DeepCopy());
            third.Text = "$500";
            third.Value = 500;
            third.Type = ThirdType.Regular;
            third.SetColor(WheelColors.BLACK, WheelColors.BLUE);
            wedge = new Wedge(third, 3);
            s.Add(wedge.DeepCopy());
            Spaces.Add(s.DeepCopy());

            third.Text = "$650";
            third.Value = 650;
            third.Type = ThirdType.Regular;
            third.SetColor(WheelColors.BLACK, WheelColors.RED);
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            third.Text = "$550";
            third.Value = 550;
            third.Type = ThirdType.Regular;
            third.SetColor(WheelColors.BLACK, WheelColors.PINK);
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            third.Text = "$450";
            third.Value = 450;
            third.Type = ThirdType.Regular;
            third.SetColor(WheelColors.BLACK, WheelColors.YELLOW);
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            third.Text = "$600";
            third.Value = 600;
            third.Type = ThirdType.Regular;
            third.SetColor(WheelColors.BLACK, WheelColors.PURPLE);
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            space = new Space();
            List<Third> list = new List<Third>();
            third.Text = "BANKRUPT";
            third.Value = 0;
            third.Type = ThirdType.Bankrupt;
            third.SetColor(WheelColors.WHITE, WheelColors.BLACK);
            list.Add(third.DeepCopy());
            colorSetText.TopColor = WheelColors.BLACK;
            colorSetText.BottomColor = Colors.LimeGreen;
            colorSetBack.TopColor = Colors.LimeGreen;
            colorSetBack.BottomColor = Colors.Black;
            third.Text = "ONE MILLION";
            third.Value = 500;
            third.Type = ThirdType.Million;
            third.SetColor(colorSetText.DeepCopy(), colorSetBack.DeepCopy());
            list.Add(third.DeepCopy());
            third.Text = "BANKRUPT";
            third.Value = 0;
            third.Type = ThirdType.Bankrupt;
            third.SetColor(WheelColors.WHITE, WheelColors.BLACK);
            list.Add(third.DeepCopy());
            wedge = new Wedge(list);
            space.Add(wedge.DeepCopy());
            third.Text = "$500";
            third.Value = 500;
            third.Type = ThirdType.Regular;
            third.SetColor(WheelColors.BLACK, WheelColors.PINK);
            wedge = new Wedge(third, 3);
            space.Add(wedge.DeepCopy());
            Spaces.Add(space);

            third.Text = "$1000";
            third.Value = 1000;
            third.Type = ThirdType.Regular;
            third.SetColor(WheelColors.BLACK, WheelColors.BLUE);
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            third.Text = "$750";
            third.Value = 750;
            third.Type = ThirdType.Regular;
            third.SetColor(WheelColors.BLACK, WheelColors.RED);
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            third.Text = "$950";
            third.Value = 950;
            third.Type = ThirdType.Regular;
            third.SetColor(WheelColors.BLACK, WheelColors.YELLOW);
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            third.Text = "$550";
            third.Value = 550;
            third.Type = ThirdType.Regular;
            third.SetColor(WheelColors.BLACK, WheelColors.TEAL);
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            colorSetText = new ColorSet(Colors.DarkRed, Colors.HotPink);
            colorSetBack = new ColorSet(Colors.LightPink, Colors.DarkRed);
            third.Text = "$4000";
            third.Value = 4000;
            third.Type = ThirdType.HighAmount;
            third.SetColor(colorSetText, colorSetBack);
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            third.Text = "BANKRUPT";
            third.Value = 0;
            third.Type = ThirdType.Bankrupt;
            third.SetColor(WheelColors.WHITE, WheelColors.BLACK);
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            third.Text = "$350";
            third.Value = 350;
            third.Type = ThirdType.Regular;
            third.SetColor(WheelColors.BLACK, WheelColors.ORANGE);
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            third.Text = "$550";
            third.Value = 550;
            third.Type = ThirdType.Regular;
            third.SetColor(WheelColors.BLACK, WheelColors.TEAL);
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            third.Text = "$500";
            third.Value = 500;
            third.Type = ThirdType.Regular;
            third.SetColor(WheelColors.BLACK, WheelColors.PINK);
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            third.Text = "$550";
            third.Value = 550;
            third.Type = ThirdType.Regular;
            third.SetColor(WheelColors.BLACK, WheelColors.PURPLE);
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            third.Text = "$850";
            third.Value = 850;
            third.Type = ThirdType.Regular;
            third.SetColor(WheelColors.BLACK, WheelColors.RED);
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            third.Text = "LOSE A TURN";
            third.Value = 0;
            third.Type = ThirdType.LoseATurn;
            third.SetColor(WheelColors.BLACK, WheelColors.WHITE);
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            third.Text = "$1000";
            third.Value = 1000;
            third.Type = ThirdType.Regular;
            third.SetColor(WheelColors.BLACK, WheelColors.BLUE);
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            third.Text = "FREE PLAY";
            third.Value = 500;
            third.Type = ThirdType.FreePlay;
            colorSetText.TopColor = Colors.Yellow;
            colorSetText.BottomColor = Colors.DarkBlue;
            colorSetBack.TopColor = Colors.LightBlue;
            colorSetBack.BottomColor = Colors.Yellow;
            third.SetColor(colorSetText.DeepCopy(), colorSetBack.DeepCopy());
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            third.Text = "$700";
            third.Value = 700;
            third.Type = ThirdType.Regular;
            third.SetColor(WheelColors.BLACK, WheelColors.PURPLE);
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));
            
            //Legacy Wheel
        
            //ColorSet colorSetText = new ColorSet(Colors.Black, Colors.LightBlue);
            //ColorSet colorSetBack = new ColorSet(Colors.LightBlue, Colors.DarkBlue);
            //third.Text = "$2500";
            //third.Value = 2500;
            //third.Type = ThirdType.HighAmount;
            //third.SetColor(colorSetText, colorSetBack);
            //wedge = new Wedge(third, 3);
            //Spaces.Add(new Space(wedge.DeepCopy()));

            //third.Text = "$500";
            //third.Value = 500;
            //third.Type = ThirdType.Regular;
            //third.SetColor(WheelColors.BLACK, WheelColors.TEAL);
            //wedge = new Wedge(third, 3);
            //Spaces.Add(new Space(wedge.DeepCopy()));

            //third.Text = "$900";
            //third.Value = 900;
            //third.Type = ThirdType.Regular;
            //third.SetColor(WheelColors.BLACK, WheelColors.YELLOW);
            //wedge = new Wedge(third, 3);
            //Spaces.Add(new Space(wedge.DeepCopy()));

            //third.Text = "$700";
            //third.Value = 700;
            //third.Type = ThirdType.Regular;
            //third.SetColor(WheelColors.BLACK, WheelColors.RED);
            //wedge = new Wedge(third, 3);
            //Spaces.Add(new Space(wedge.DeepCopy()));

            //third.Text = "$600";
            //third.Value = 600;
            //third.Type = ThirdType.Regular;
            //third.SetColor(WheelColors.BLACK, WheelColors.BLUE);
            //wedge = new Wedge(third, 3);
            //Spaces.Add(new Space(wedge.DeepCopy()));

            //third.Text = "$800";
            //third.Value = 800;
            //third.Type = ThirdType.Regular;
            //third.SetColor(WheelColors.BLACK, WheelColors.ORANGE);
            //wedge = new Wedge(third, 3);
            //Spaces.Add(new Space(wedge.DeepCopy()));

            //third.Text = "$500";
            //third.Value = 500;
            //third.Type = ThirdType.Regular;
            //third.SetColor(WheelColors.BLACK, WheelColors.PURPLE);
            //wedge = new Wedge(third, 3);
            //Spaces.Add(new Space(wedge.DeepCopy()));

            //third.Text = "$700";
            //third.Value = 700;
            //third.Type = ThirdType.Regular;
            //third.SetColor(WheelColors.BLACK, WheelColors.YELLOW);
            //wedge = new Wedge(third, 3);
            //Spaces.Add(new Space(wedge.DeepCopy()));

            //space = new Space();
            //List<Third> list = new List<Third>();
            //third.Text = "BANKRUPT";
            //third.Value = 0;
            //third.Type = ThirdType.Bankrupt;
            //third.SetColor(WheelColors.WHITE, WheelColors.BLACK);
            //list.Add(third.DeepCopy());
            //colorSetText.TopColor = WheelColors.BLACK;
            //colorSetText.BottomColor = Colors.LimeGreen;
            //colorSetBack.TopColor = Colors.LimeGreen;
            //colorSetBack.BottomColor = Colors.Black;
            //third.Text = "ONE MILLION";
            //third.Value = 500;
            //third.Type = ThirdType.Million;
            //third.SetColor(colorSetText.DeepCopy(), colorSetBack.DeepCopy());
            //list.Add(third.DeepCopy());
            //third.Text = "BANKRUPT";
            //third.Value = 0;
            //third.Type = ThirdType.Bankrupt;
            //third.SetColor(WheelColors.WHITE, WheelColors.BLACK);
            //list.Add(third.DeepCopy());
            //wedge = new Wedge(list);
            //space.Add(wedge.DeepCopy());
            //third.Text = "$500";
            //third.Value = 500;
            //third.Type = ThirdType.Regular;
            //third.SetColor(WheelColors.BLACK, WheelColors.PINK);
            //wedge = new Wedge(third, 3);
            //space.Add(wedge.DeepCopy());
            //Spaces.Add(space);

            //third.Text = "$600";
            //third.Value = 600;
            //third.Type = ThirdType.Regular;
            //third.SetColor(WheelColors.BLACK, WheelColors.RED);
            //wedge = new Wedge(third, 3);
            //Spaces.Add(new Space(wedge.DeepCopy()));

            //third.Text = "$550";
            //third.Value = 550;
            //third.Type = ThirdType.Regular;
            //third.SetColor(WheelColors.BLACK, WheelColors.BLUE);
            //wedge = new Wedge(third, 3);
            //Spaces.Add(new Space(wedge.DeepCopy()));

            //third.Text = "$500";
            //third.Value = 500;
            //third.Type = ThirdType.Regular;
            //third.SetColor(WheelColors.BLACK, WheelColors.TEAL);
            //wedge = new Wedge(third, 3);
            //Spaces.Add(new Space(wedge.DeepCopy()));

            //third.Text = "$900";
            //third.Value = 900;
            //third.Type = ThirdType.Regular;
            //third.SetColor(WheelColors.BLACK, WheelColors.PINK);
            //wedge = new Wedge(third, 3);
            //Spaces.Add(new Space(wedge.DeepCopy()));

            //third.Text = "BANKRUPT";
            //third.Value = 0;
            //third.Type = ThirdType.Bankrupt;
            //third.SetColor(WheelColors.WHITE, WheelColors.BLACK);
            //wedge = new Wedge(third, 3);
            //Spaces.Add(new Space(wedge.DeepCopy()));

            //third.Text = "$650";
            //third.Value = 650;
            //third.Type = ThirdType.Regular;
            //third.SetColor(WheelColors.BLACK, WheelColors.PURPLE);
            //wedge = new Wedge(third, 3);
            //Spaces.Add(new Space(wedge.DeepCopy()));

            //third.Text = "FREE PLAY";
            //third.Value = 500;
            //third.Type = ThirdType.FreePlay;
            //colorSetText.TopColor = Colors.Yellow;
            //colorSetText.BottomColor = Colors.DarkBlue;
            //colorSetBack.TopColor = Colors.LightBlue;
            //colorSetBack.BottomColor = Colors.Yellow;
            //third.SetColor(colorSetText.DeepCopy(), colorSetBack.DeepCopy());
            //wedge = new Wedge(third, 3);
            //Spaces.Add(new Space(wedge.DeepCopy()));

            //third.Text = "$700";
            //third.Value = 700;
            //third.Type = ThirdType.Regular;
            //third.SetColor(WheelColors.BLACK, WheelColors.BLUE);
            //wedge = new Wedge(third, 3);
            //Spaces.Add(new Space(wedge.DeepCopy()));

            //third.Text = "LOSE A TURN";
            //third.Value = 0;
            //third.Type = ThirdType.LoseATurn;
            //third.SetColor(WheelColors.BLACK, WheelColors.WHITE);
            //wedge = new Wedge(third, 3);
            //Spaces.Add(new Space(wedge.DeepCopy()));

            //third.Text = "$800";
            //third.Value = 800;
            //third.Type = ThirdType.Regular;
            //third.SetColor(WheelColors.BLACK, WheelColors.RED);
            //wedge = new Wedge(third, 3);
            //Spaces.Add(new Space(wedge.DeepCopy()));

            //Space s = new Space();
            //third.Text = "PRIZE";
            //third.Value = 500;
            //third.Type = ThirdType.Prize;
            //colorSetText.TopColor = WheelColors.BLACK;
            //colorSetText.BottomColor = Colors.White;
            //colorSetBack.TopColor = Colors.LightBlue;
            //colorSetBack.BottomColor = Colors.DarkSlateBlue;
            //third.SetColor(colorSetText.DeepCopy(), colorSetBack.DeepCopy());
            //wedge = new Wedge(third, 3);
            //s.Add(wedge.DeepCopy());
            //third.Text = "$500";
            //third.Value = 500;
            //third.Type = ThirdType.Regular;
            //third.SetColor(WheelColors.BLACK, WheelColors.YELLOW);
            //wedge = new Wedge(third, 3);
            //s.Add(wedge.DeepCopy());
            //Spaces.Add(s.DeepCopy());

            //third.Text = "$650";
            //third.Value = 650;
            //third.Type = ThirdType.Regular;
            //third.SetColor(WheelColors.BLACK, WheelColors.PINK);
            //wedge = new Wedge(third, 3);
            //Spaces.Add(new Space(wedge.DeepCopy()));

            //third.Text = "$500";
            //third.Value = 500;
            //third.Type = ThirdType.Regular;
            //third.SetColor(WheelColors.BLACK, WheelColors.TEAL);
            //wedge = new Wedge(third, 3);
            //Spaces.Add(new Space(wedge.DeepCopy()));

            //third.Text = "$900";
            //third.Value = 900;
            //third.Type = ThirdType.Regular;
            //third.SetColor(WheelColors.BLACK, WheelColors.ORANGE);
            //wedge = new Wedge(third, 3);
            //Spaces.Add(new Space(wedge.DeepCopy()));

            //third.Text = "BANKRUPT";
            //third.Value = 0;
            //third.Type = ThirdType.Bankrupt;
            //third.SetColor(WheelColors.WHITE, WheelColors.BLACK);
            //wedge = new Wedge(third, 3);
            //Spaces.Add(new Space(wedge.DeepCopy()));
        }
    }
}
