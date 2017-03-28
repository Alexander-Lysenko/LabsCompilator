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

ep	:'+' t {_stack.Push(_stack.pop()+_stack.pop());} ep
	|'-' t {double a = _stack.Pop();_stack.Push(_stack.Pop()-a);} ep
	;
	
t	:f tp
	;

tp	:'*' f {_stack.Push(_stack.Pop()*_stack.Pop())} tp
	|'/' f {double a = _stack.pop();_stack.Push(_stack.Pop()/a);} tp
	|'^' f {double a = _stack.pop();_stack.Push(Math.Pow(_stack.pop(),a));} tp
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

