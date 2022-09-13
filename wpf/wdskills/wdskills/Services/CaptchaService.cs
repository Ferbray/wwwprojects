using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wdskills.Services
{
    public class CaptchaService
    {
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private Brush[] colorsChar = {
            new SolidBrush(Color.FromArgb(235, 134, 134)),
            new SolidBrush(Color.FromArgb(235, 134, 164)),
            new SolidBrush(Color.FromArgb(235, 134, 201)),
            new SolidBrush(Color.FromArgb(226, 134, 235)),
            new SolidBrush(Color.FromArgb(191, 134, 235)),
            new SolidBrush(Color.FromArgb(154, 134, 235)),
            new SolidBrush(Color.FromArgb(134, 162, 235)),
            new SolidBrush(Color.FromArgb(134, 186, 235)),
            new SolidBrush(Color.FromArgb(134, 223, 235)),
            new SolidBrush(Color.FromArgb(134, 235, 186)),
            new SolidBrush(Color.FromArgb(134, 235, 137)),
            new SolidBrush(Color.FromArgb(172, 235, 134)),
            new SolidBrush(Color.FromArgb(214, 235, 134)),
            new SolidBrush(Color.FromArgb(235, 206, 134)),
            new SolidBrush(Color.FromArgb(235, 157, 134)),
        };
        private Brush[] colorsBackground = {
            new SolidBrush(Color.FromArgb(56, 6, 6)),
            new SolidBrush(Color.FromArgb(56, 6, 27)),
            new SolidBrush(Color.FromArgb(56, 6, 39)),
            new SolidBrush(Color.FromArgb(43, 6, 56)),
            new SolidBrush(Color.FromArgb(25, 6, 56)),
            new SolidBrush(Color.FromArgb(6, 6, 56)),
            new SolidBrush(Color.FromArgb(6, 26, 56)),
            new SolidBrush(Color.FromArgb(6, 44, 56)),
            new SolidBrush(Color.FromArgb(6, 56, 47)),
            new SolidBrush(Color.FromArgb(6, 56, 29)),
            new SolidBrush(Color.FromArgb(6, 56, 7)),
            new SolidBrush(Color.FromArgb(25, 56, 6)),
            new SolidBrush(Color.FromArgb(46, 56, 6)),
            new SolidBrush(Color.FromArgb(56, 42, 6)),
            new SolidBrush(Color.FromArgb(56, 27, 6)),
        };
        private readonly Random _random = new();

        public async Task<string> CreateCaptcha(int width, int height)
        {
            Bitmap result = new(width, height);
            int Xpos = _random.Next(0, 65);
            int Ypos = _random.Next(15, height/2-height/3);
            Graphics g = Graphics.FromImage((Image)result);
            Color backRandColor = ((SolidBrush)colorsBackground[_random.Next(colorsBackground.Length)]).Color;
            g.Clear(backRandColor);
            string textCaptcha = new string(Enumerable.Repeat(chars, 6).Select(s => s[_random.Next(s.Length)]).ToArray());
            foreach(char symbol in textCaptcha)
            {
                g.DrawString(
                    symbol.ToString(),
                    new Font("Comic Sans MS", 36),
                    colorsChar[_random.Next(0, colorsChar.Length)],
                    new PointF(Xpos, Ypos));
                Xpos += 36;
                Ypos = _random.Next(10, height / 3);
            }
            
            g.DrawLine(
                new Pen(Color.Black, 5),
                new Point(0, height/2),
                new Point(width - 1, height/2));

            string path = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\net6.0-windows", "");

            using (FileStream image = new ($"{path}wwwroot\\captcha.jpg", FileMode.Create))
            {
                ImageConverter imageConverter = new();
                var bytes = (byte[])imageConverter.ConvertTo(result, typeof(byte[]))!;
                await image.WriteAsync(bytes);
            }

            return textCaptcha;
        }
    }
}
