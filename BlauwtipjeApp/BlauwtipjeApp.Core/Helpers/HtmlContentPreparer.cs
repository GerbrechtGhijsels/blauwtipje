using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using BlauwtipjeApp.Core.Interfaces;
using BlauwtipjeApp.Core.Models.Tree;

namespace BlauwtipjeApp.Core.Helpers
{
    public class HtmlContentPreparer : IHtmlContentPreparer
    {
        private readonly List<Image> Images;

        public HtmlContentPreparer(List<Image> images)
        {
            Images = images ?? new List<Image>();
        }

        public string InjectImages(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return "";
            var match = Regex.Match(text, @"{img:(?<imgId>\d+)}");
            var matches = new List<Match>();
            var imageIds = new List<int>();
            while (match.Success)
            {
                if (int.TryParse(match.Groups["imgId"].Value, out var imageId))
                {
                    matches.Add(match);
                    imageIds.Add(imageId);
                }
                match = match.NextMatch();
            }

            var images = new List<Image>();
            foreach (var id in imageIds)
                images.Add(Images.Find((image) => image.XmlId == id));

            var imagesAndMatches = images.Zip(matches, (_image, _match) => new { Image = _image, Match = _match });
            foreach (var imageAndMatch in imagesAndMatches)
            {
                var _image = imageAndMatch.Image;
                var _match = imageAndMatch.Match;
                if (_image == null) continue;
                var regex = new Regex(_match.Value, RegexOptions.IgnoreCase);
                text = regex.Replace(text, "base64," + Convert.ToBase64String(_image.Content), 1);
            }

            return text;
        }
    }
}
