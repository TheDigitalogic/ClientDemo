import React from 'react';
import './button.css';

function Button({label}){
 return <div id="button" data-testid="button" className='button-style'>{label}</div>
}

export default Button;