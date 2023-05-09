
using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.commons;
using System.Windows.Forms;

namespace com.calitha.goldparser
{

    [Serializable()]
    public class SymbolException : System.Exception
    {
        public SymbolException(string message) : base(message)
        {
        }

        public SymbolException(string message,
            Exception inner) : base(message, inner)
        {
        }

        protected SymbolException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

    }

    [Serializable()]
    public class RuleException : System.Exception
    {

        public RuleException(string message) : base(message)
        {
        }

        public RuleException(string message,
                             Exception inner) : base(message, inner)
        {
        }

        protected RuleException(SerializationInfo info,
                                StreamingContext context) : base(info, context)
        {
        }

    }

    enum SymbolConstants : int
    {
        SYMBOL_EOF        =  0, // (EOF)
        SYMBOL_ERROR      =  1, // (Error)
        SYMBOL_WHITESPACE =  2, // Whitespace
        SYMBOL_MINUS      =  3, // '-'
        SYMBOL_MINUSMINUS =  4, // '--'
        SYMBOL_EXCLAMEQ   =  5, // '!='
        SYMBOL_PERCENT    =  6, // '%'
        SYMBOL_LPAREN     =  7, // '('
        SYMBOL_RPAREN     =  8, // ')'
        SYMBOL_TIMES      =  9, // '*'
        SYMBOL_TIMESTIMES = 10, // '**'
        SYMBOL_COMMA      = 11, // ','
        SYMBOL_DIV        = 12, // '/'
        SYMBOL_SEMI       = 13, // ';'
        SYMBOL_PLUS       = 14, // '+'
        SYMBOL_PLUSPLUS   = 15, // '++'
        SYMBOL_LT         = 16, // '<'
        SYMBOL_EQ         = 17, // '='
        SYMBOL_EQEQ       = 18, // '=='
        SYMBOL_GT         = 19, // '>'
        SYMBOL_BEGIN      = 20, // Begin
        SYMBOL_DIGIT      = 21, // DIGIT
        SYMBOL_DOUBLE     = 22, // double
        SYMBOL_ELSE       = 23, // else
        SYMBOL_END        = 24, // end
        SYMBOL_FINISH     = 25, // Finish
        SYMBOL_FLOAT      = 26, // float
        SYMBOL_FOR        = 27, // For
        SYMBOL_IDENTIFIER = 28, // IDENTIFIER
        SYMBOL_IF         = 29, // if
        SYMBOL_INT        = 30, // int
        SYMBOL_STR        = 31, // str
        SYMBOL_ASSIGN     = 32, // <assign>
        SYMBOL_CONCEPT    = 33, // <concept>
        SYMBOL_COND       = 34, // <cond>
        SYMBOL_DATA       = 35, // <data>
        SYMBOL_DIGIT2     = 36, // <digit>
        SYMBOL_EXP        = 37, // <exp>
        SYMBOL_EXPR       = 38, // <expr>
        SYMBOL_FACTOR     = 39, // <factor>
        SYMBOL_FOR_STMT   = 40, // <for_stmt>
        SYMBOL_ID         = 41, // <id>
        SYMBOL_IF_STMT    = 42, // <if_stmt>
        SYMBOL_OP         = 43, // <op>
        SYMBOL_PROGRAM    = 44, // <program>
        SYMBOL_STEP       = 45, // <step>
        SYMBOL_STMT_LIST  = 46, // <stmt_list>
        SYMBOL_TERM       = 47  // <term>
    };

    enum RuleConstants : int
    {
        RULE_PROGRAM_BEGIN_FINISH                           =  0, // <program> ::= Begin <stmt_list> Finish
        RULE_STMT_LIST                                      =  1, // <stmt_list> ::= <concept>
        RULE_STMT_LIST2                                     =  2, // <stmt_list> ::= <concept> <stmt_list>
        RULE_CONCEPT                                        =  3, // <concept> ::= <assign>
        RULE_CONCEPT2                                       =  4, // <concept> ::= <if_stmt>
        RULE_CONCEPT3                                       =  5, // <concept> ::= <for_stmt>
        RULE_ASSIGN_EQ_COMMA                                =  6, // <assign> ::= <id> '=' <expr> ','
        RULE_ID_IDENTIFIER                                  =  7, // <id> ::= IDENTIFIER
        RULE_EXPR_PLUS                                      =  8, // <expr> ::= <expr> '+' <term>
        RULE_EXPR_MINUS                                     =  9, // <expr> ::= <expr> '-' <term>
        RULE_EXPR                                           = 10, // <expr> ::= <term>
        RULE_TERM_TIMES                                     = 11, // <term> ::= <term> '*' <factor>
        RULE_TERM_DIV                                       = 12, // <term> ::= <term> '/' <factor>
        RULE_TERM_PERCENT                                   = 13, // <term> ::= <term> '%' <factor>
        RULE_TERM                                           = 14, // <term> ::= <factor>
        RULE_FACTOR_TIMESTIMES                              = 15, // <factor> ::= <factor> '**' <exp>
        RULE_FACTOR                                         = 16, // <factor> ::= <exp>
        RULE_EXP_LPAREN_RPAREN                              = 17, // <exp> ::= '(' <expr> ')'
        RULE_EXP                                            = 18, // <exp> ::= <id>
        RULE_EXP2                                           = 19, // <exp> ::= <digit>
        RULE_DIGIT_DIGIT                                    = 20, // <digit> ::= DIGIT
        RULE_IF_STMT_IF_LPAREN_RPAREN_BEGIN_END             = 21, // <if_stmt> ::= if '(' <cond> ')' Begin <stmt_list> end
        RULE_IF_STMT_IF_LPAREN_RPAREN_BEGIN_END_ELSE        = 22, // <if_stmt> ::= if '(' <cond> ')' Begin <stmt_list> end else <stmt_list>
        RULE_COND                                           = 23, // <cond> ::= <exp> <op> <exp>
        RULE_OP_LT                                          = 24, // <op> ::= '<'
        RULE_OP_GT                                          = 25, // <op> ::= '>'
        RULE_OP_EQEQ                                        = 26, // <op> ::= '=='
        RULE_OP_EXCLAMEQ                                    = 27, // <op> ::= '!='
        RULE_FOR_STMT_FOR_LPAREN_SEMI_SEMI_RPAREN_BEGIN_END = 28, // <for_stmt> ::= For '(' <data> <assign> ';' <cond> ';' <step> ')' Begin <stmt_list> end
        RULE_DATA_INT                                       = 29, // <data> ::= int
        RULE_DATA_FLOAT                                     = 30, // <data> ::= float
        RULE_DATA_DOUBLE                                    = 31, // <data> ::= double
        RULE_DATA_STR                                       = 32, // <data> ::= str
        RULE_STEP_MINUSMINUS                                = 33, // <step> ::= '--' <id>
        RULE_STEP_MINUSMINUS2                               = 34, // <step> ::= <id> '--'
        RULE_STEP_PLUSPLUS                                  = 35, // <step> ::= '++' <id>
        RULE_STEP_PLUSPLUS2                                 = 36, // <step> ::= <id> '++'
        RULE_STEP                                           = 37  // <step> ::= <assign>
    };

    public class MyParser
    {
        private LALRParser parser;
        ListBox lstErrors;

        public MyParser(string filename, ListBox lstErrors)
        {
            FileStream stream = new FileStream(filename,
                                               FileMode.Open, 
                                               FileAccess.Read, 
                                               FileShare.Read);
            Init(stream);
            stream.Close();

            this.lstErrors = lstErrors;
        }

        public MyParser(string baseName, string resourceName)
        {
            byte[] buffer = ResourceUtil.GetByteArrayResource(
                System.Reflection.Assembly.GetExecutingAssembly(),
                baseName,
                resourceName);
            MemoryStream stream = new MemoryStream(buffer);
            Init(stream);
            stream.Close();
        }

        public MyParser(Stream stream)
        {
            Init(stream);
        }

        private void Init(Stream stream)
        {
            CGTReader reader = new CGTReader(stream);
            parser = reader.CreateNewParser();
            parser.TrimReductions = false;
            parser.StoreTokens = LALRParser.StoreTokensMode.NoUserObject;

            parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
            parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);
        }

        public void Parse(string source)
        {
            NonterminalToken token = parser.Parse(source);
            if (token != null)
            {
                Object obj = CreateObject(token);
                //todo: Use your object any way you like
            }
        }

        private Object CreateObject(Token token)
        {
            if (token is TerminalToken)
                return CreateObjectFromTerminal((TerminalToken)token);
            else
                return CreateObjectFromNonterminal((NonterminalToken)token);
        }

        private Object CreateObjectFromTerminal(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)SymbolConstants.SYMBOL_EOF :
                //(EOF)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ERROR :
                //(Error)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHITESPACE :
                //Whitespace
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUS :
                //'-'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUSMINUS :
                //'--'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXCLAMEQ :
                //'!='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PERCENT :
                //'%'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LPAREN :
                //'('
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RPAREN :
                //')'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMES :
                //'*'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMESTIMES :
                //'**'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COMMA :
                //','
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIV :
                //'/'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SEMI :
                //';'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUS :
                //'+'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUSPLUS :
                //'++'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LT :
                //'<'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQ :
                //'='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQEQ :
                //'=='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GT :
                //'>'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_BEGIN :
                //Begin
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIGIT :
                //DIGIT
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DOUBLE :
                //double
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ELSE :
                //else
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_END :
                //end
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FINISH :
                //Finish
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FLOAT :
                //float
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR :
                //For
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IDENTIFIER :
                //IDENTIFIER
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF :
                //if
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INT :
                //int
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STR :
                //str
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ASSIGN :
                //<assign>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CONCEPT :
                //<concept>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COND :
                //<cond>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DATA :
                //<data>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIGIT2 :
                //<digit>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXP :
                //<exp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXPR :
                //<expr>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FACTOR :
                //<factor>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR_STMT :
                //<for_stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ID :
                //<id>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF_STMT :
                //<if_stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_OP :
                //<op>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PROGRAM :
                //<program>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STEP :
                //<step>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STMT_LIST :
                //<stmt_list>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TERM :
                //<term>
                //todo: Create a new object that corresponds to the symbol
                return null;

            }
            throw new SymbolException("Unknown symbol");
        }

        public Object CreateObjectFromNonterminal(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)RuleConstants.RULE_PROGRAM_BEGIN_FINISH :
                //<program> ::= Begin <stmt_list> Finish
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT_LIST :
                //<stmt_list> ::= <concept>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT_LIST2 :
                //<stmt_list> ::= <concept> <stmt_list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT :
                //<concept> ::= <assign>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT2 :
                //<concept> ::= <if_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT3 :
                //<concept> ::= <for_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ASSIGN_EQ_COMMA :
                //<assign> ::= <id> '=' <expr> ','
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ID_IDENTIFIER :
                //<id> ::= IDENTIFIER
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR_PLUS :
                //<expr> ::= <expr> '+' <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR_MINUS :
                //<expr> ::= <expr> '-' <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR :
                //<expr> ::= <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_TIMES :
                //<term> ::= <term> '*' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_DIV :
                //<term> ::= <term> '/' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_PERCENT :
                //<term> ::= <term> '%' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM :
                //<term> ::= <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR_TIMESTIMES :
                //<factor> ::= <factor> '**' <exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR :
                //<factor> ::= <exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP_LPAREN_RPAREN :
                //<exp> ::= '(' <expr> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP :
                //<exp> ::= <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP2 :
                //<exp> ::= <digit>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DIGIT_DIGIT :
                //<digit> ::= DIGIT
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IF_STMT_IF_LPAREN_RPAREN_BEGIN_END :
                //<if_stmt> ::= if '(' <cond> ')' Begin <stmt_list> end
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IF_STMT_IF_LPAREN_RPAREN_BEGIN_END_ELSE :
                //<if_stmt> ::= if '(' <cond> ')' Begin <stmt_list> end else <stmt_list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_COND :
                //<cond> ::= <exp> <op> <exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_LT :
                //<op> ::= '<'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_GT :
                //<op> ::= '>'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_EQEQ :
                //<op> ::= '=='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_EXCLAMEQ :
                //<op> ::= '!='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FOR_STMT_FOR_LPAREN_SEMI_SEMI_RPAREN_BEGIN_END :
                //<for_stmt> ::= For '(' <data> <assign> ';' <cond> ';' <step> ')' Begin <stmt_list> end
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DATA_INT :
                //<data> ::= int
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DATA_FLOAT :
                //<data> ::= float
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DATA_DOUBLE :
                //<data> ::= double
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DATA_STR :
                //<data> ::= str
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_MINUSMINUS :
                //<step> ::= '--' <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_MINUSMINUS2 :
                //<step> ::= <id> '--'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_PLUSPLUS :
                //<step> ::= '++' <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_PLUSPLUS2 :
                //<step> ::= <id> '++'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP :
                //<step> ::= <assign>
                //todo: Create a new object using the stored tokens.
                return null;

            }
            throw new RuleException("Unknown rule");
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            string message = "Token error with input: '"+args.Token.ToString()+"'";
            lstErrors.Items.Add(message);
            //todo: Report message to UI?
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string message = "Parse error caused by token: '"+args.UnexpectedToken.ToString()+"'" +" in line: "+args.UnexpectedToken.Location.LineNr;
            lstErrors.Items.Add(message);
            //todo: Report message to UI?
            string message2 = "Expected token : '" + args.ExpectedTokens.ToString() + "'";
            lstErrors.Items.Add(message2);
        }

    }
}
