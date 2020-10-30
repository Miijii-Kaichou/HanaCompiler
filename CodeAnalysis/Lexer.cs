using System.Collections.Generic;

namespace Hana.CodeAnalysis
{
    internal sealed class Lexer
    {
        private readonly string _text;
        private int _position;

        private List<string> _diagnostics = new List<string>();
        public Lexer(string text)
        {
            _text = text;
        }

        public IEnumerable<string> Diagnostics => _diagnostics;

        private char Current
        {
            get
            {
                //Return nothing
                if (_position >= _text.Length)
                    return '\0';

                //Get the current character
                return _text[_position];
            }
        }

        private int Next()
        {
            //Go to next character
            return _position++;
        }

        private SyntaxToken ThrowError(out string errorMessage)
        {
            string _errorMSG = $"ERROR : bad character input: '{Current }'";
            errorMessage = _errorMSG;
            _diagnostics.Add(_errorMSG);
            return new SyntaxToken(SyntaxKind.BadToken, Next(), _text.Substring(_position - 1, 1), null);
        }

        public SyntaxToken NextToken()
        {
            // Numbers
            // + - * / ( ) { } [ ]
            // whitespace

            if (_position >= _text.Length)
                return new SyntaxToken(SyntaxKind.EOFToken, _position, "\0", null);

            if (char.IsDigit(Current))
            {
                var start = _position;

                while (char.IsDigit(Current))
                    Next();

                var length = _position - start;
                var text = _text.Substring(start, length);
                if (!int.TryParse(text, out var value))
                {
                    _diagnostics.Add($"The number {_text} cannot be represented by an Int32");
                }



                return new SyntaxToken(SyntaxKind.NumberToken, start, text, value);
            }

            if (char.IsWhiteSpace(Current))
            {
                var start = _position;
                while (char.IsWhiteSpace(Current))
                {
                    Next();
                }

                var length = _position - start;
                var text = _text.Substring(start, length);
                return new SyntaxToken(SyntaxKind.WhiteSpaceToken, start, text, null);
            }

            return Current switch
            {
                '+' => new SyntaxToken(SyntaxKind.PlusToken, Next(), "+", null),
                '-' => new SyntaxToken(SyntaxKind.MinusToken, Next(), "-", null),
                '*' => new SyntaxToken(SyntaxKind.MultiplyToken, Next(), "*", null),
                '/' => new SyntaxToken(SyntaxKind.DivideToken, Next(), "/", null),
                '(' => new SyntaxToken(SyntaxKind.LParenToken, Next(), "(", null),
                ')' => new SyntaxToken(SyntaxKind.RParenToken, Next(), ")", null),
                '[' => new SyntaxToken(SyntaxKind.LBrackToken, Next(), "[", null),
                ']' => new SyntaxToken(SyntaxKind.RBrackToken, Next(), "]", null),
                '{' => new SyntaxToken(SyntaxKind.LCurlToken, Next(), "{", null),
                '}' => new SyntaxToken(SyntaxKind.RCurlToken, Next(), "}", null),
                _ => ThrowError(out string errorMSG),
            };
        }
    }
}
