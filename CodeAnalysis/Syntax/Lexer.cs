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

        private char Current => Peek(0);
        private char Ahead => Peek(1);
        private char Peek(int offset)
        {
            var index = _position + offset;

            //Return nothing
            if (index >= _text.Length)
                return '\0';

            //Get the current character
            return _text[index];
        }

        private int Next()
        {
            //Go to next character
            return _position++;
        }

        public SyntaxToken Lex()
        {
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

            if (char.IsLetter(Current))
            {
                var start = _position;
                while (char.IsLetter(Current))
                {
                    Next();
                }

                var length = _position - start;
                var text = _text.Substring(start, length);
                var kind = SyntaxFacts.GetKeywordKind(text);
                return new SyntaxToken(kind, start, text, null);
            }
            // True 
            // False

            switch (Current)
            {
                case '+': return new SyntaxToken(SyntaxKind.PlusToken, Next(), "+", null);
                case '-': return new SyntaxToken(SyntaxKind.MinusToken, Next(), "-", null);
                case '*': return new SyntaxToken(SyntaxKind.StarToken, Next(), "*", null);
                case '/': return new SyntaxToken(SyntaxKind.FSlashToken, Next(), "/", null);
                case '(': return new SyntaxToken(SyntaxKind.LParenToken, Next(), "(", null);
                case ')': return new SyntaxToken(SyntaxKind.RParenToken, Next(), ")", null);
                case '[': return new SyntaxToken(SyntaxKind.LBrackToken, Next(), "[", null);
                case ']': return new SyntaxToken(SyntaxKind.RBrackToken, Next(), "]", null);
                case '{': return new SyntaxToken(SyntaxKind.LCurlToken, Next(), "{", null);
                case '}': return new SyntaxToken(SyntaxKind.RCurlToken, Next(), "}", null);
                case '<':
                    {
                        // <<
                        if (Ahead == '<')
                            return new SyntaxToken(SyntaxKind.DoubleLArrowToken, _position += 2, "<<", null);
                        return new SyntaxToken(SyntaxKind.LArrowToken, Next(), "<", null);
                    }
                case '>': return new SyntaxToken(SyntaxKind.RArrowToken, Next(), ">", null);
                case '!':
                    {
                        // !=
                        if (Ahead == '=')
                            return new SyntaxToken(SyntaxKind.InEqulityToken, _position += 2, "!=", null);
                        return new SyntaxToken(SyntaxKind.ExclamToken, Next(), "!", null);
                    }
                    
                case '&':
                    {
                        // &&
                        if (Ahead == '&')
                            return new SyntaxToken(SyntaxKind.AndToken, _position += 2, "&&", null);

                        return new SyntaxToken(SyntaxKind.AmpToken, Next(), "&", null);
                    }
                case '|':
                    {
                        // ||
                        if (Ahead == '|')
                            return new SyntaxToken(SyntaxKind.OrToken, _position += 2, "||", null);
                        return new SyntaxToken(SyntaxKind.PipeToken, Next(), "|", null);
                    }
                case '=':
                    {
                        // ==
                        if (Ahead == '=')
                            return new SyntaxToken(SyntaxKind.EquilityToken, _position += 2, "==", null);
                        return new SyntaxToken(SyntaxKind.PipeToken, Next(), "=", null);
                    }
                default:
                    _diagnostics.Add($"ERROR : bad character input: '{Current }'");
                    return new SyntaxToken(SyntaxKind.BadToken, Next(), _text.Substring(_position - 1, 1), null);
            }
        }
    }
}
