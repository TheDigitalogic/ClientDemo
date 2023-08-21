import React, { createElement } from "react";
import ReactDOM from 'react-dom';
import Button from "./../button";
import {render} from '@testing-library/react';
import { hydrateRoot } from 'react-dom/client';

//Test Case 1
it("renders without error", () =>
{
   const container = document.createElement("div");
   const root = hydrateRoot(container, <Button></Button>);

})

//Test Case 2
test("renders button with proper text", () =>{
    const {getByTestId} = render(<Button label="Click me"></Button>);

     expect(getByTestId("button")).toHaveTextContent("Click me");
})

//Test Case 3
//*** Expected Faild Test Case for "Button" *****************
// test("renders button incorrectly", () =>{
//     const {getByTestId} = render(<Button label="Click me"></Button>);

//      expect(getByTestId("button")).toHaveTextContent("Click me 123");
// })