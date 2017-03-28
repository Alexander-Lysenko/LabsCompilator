grammar GoA;
options
{
	language = CSharp3;
}
@parser::namespace { Generated }
@lexer::namespace  { Generated }
@header{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	
}
@members{
	//private static Hashtable memory = new Hashtable();
	private Stack<double> _stack = new Stack<double>();
}
	
	
public calc returns[Stack<double> value]
	:  e {$value = _stack;}
	;

e	:t ep
	;

ep	:'+'t ep {_stack.Push(_stack.pop()+_stack.pop());}
	|'-'t ep {double a = _stack.Pop();_stack.Push(_stack.Pop()-a);}
	;
	
t	:f tp
	;

tp	:'*' f tp {_stack.Push(_stack.Pop()*_stack.Pop())}
	|'/' f tp {double a = _stack.pop();_stack.Push(_stack.Pop()/a);}
	|'^' f tp {double a = _stack.pop();_stack.Push(Math.Pow(_stack.pop(),a));}
	;

f	:'-' fp {_stack.Push(-_stack.pop())}
	|'+'? fp
	;

fp	:a1=FLOAT {_stack.Push(double.Pars($a1.value));}
	|'('e')'
	|'sqrt('e')' {_stack.Push(Math.Sqrt(_stack.pop()));;}
	;

FLOAT
    :   ('0'..'9')+ '.' ('0'..'9')* EXPONENT?
    |   '.' ('0'..'9')+ EXPONENT?
    |   ('0'..'9')+ EXPONENT
    ;

fragment
EXPONENT : ('e'|'E') ('+'|'-')? ('0'..'9')+ ;

