using System.Collections.Generic;

namespace Hana.CodeAnalysis.Syntax
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

        private int Lex()
        {
            //Go to next character
            return _position++;
        }

        private SyntaxToken ThrowTokenError(out string errorMessage)
        {
            string _errorMSG = $"ERROR : bad character input: '{Current }'";
            errorMessage = _errorMSG;
            _diagnostics.Add(_errorMSG);
            return new SyntaxToken(SyntaxKind.BadToken, Lex(), _text.Substring(_position - 1, 1), null);
        }

        public SyntaxToken NextToken()
        {
            if (_position >= _text.Length)
                return new SyntaxToken(SyntaxKind.EOFToken, _position, "\0", null);

            if (char.IsDigit(Current))
            {
                var start = _position;

                while (char.IsDigit(Current))
                    Lex();

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
                    Lex();
                }

                var length = _position - start;
                var text = _text.Substring(start, length);
                return new SyntaxToken(SyntaxKind.WhiteSpaceToken, start, text, null);
            }

            if (char.IsLetter(Current))
            {
                var start = _position;
                while (char.IsLetter(Current))
                {
                    Lex();
                }

                var length = _position - start;
                var text = _text.Substring(start, length);
                var kind = SyntaxFacts.GetKeywordKind(text);
                return new SyntaxToken(kind, start, text, null);
            }
            // True 
            // False

            return Current switch
            {
                '+' => new SyntaxToken(SyntaxKind.PlusToken, Lex(), "+", null),
                '-' => new SyntaxToken(SyntaxKind.MinusToken, Lex(), "-", null),
                '*' => new SyntaxToken(SyntaxKind.StarToken, Lex(), "*", null),
                '/' => new SyntaxToken(SyntaxKind.FSlashToken, Lex(), "/", null),
                '(' => new SyntaxToken(SyntaxKind.LParenToken, Lex(), "(", null),
                ')' => new SyntaxToken(SyntaxKind.RParenToken, Lex(), ")", null),
                '[' => new SyntaxToken(SyntaxKind.LBrackToken, Lex(), "[", null),
                ']' => new SyntaxToken(SyntaxKind.RBrackToken, Lex(), "]", null),
                '{' => new SyntaxToken(SyntaxKind.LCurlToken, Lex(), "{", null),
                '}' => new SyntaxToken(SyntaxKind.RCurlToken, Lex(), "}", null),
                _ => ThrowTokenError(out string errorMSG),
            };
        }
    }
}
