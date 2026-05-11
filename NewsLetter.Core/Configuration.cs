using System;
using System.Collections.Generic;
using System.Text;

namespace NewsLetter.Core
{
    public static class Configuration
    {
        public static string RootPath { get; set; } = string.Empty;
        public static OpenAiConfiguration OpenAi { get; set; } = new();
    }

    public class OpenAiConfiguration
    {
        public string ApiKey { get; set; } = string.Empty;
    }
}
