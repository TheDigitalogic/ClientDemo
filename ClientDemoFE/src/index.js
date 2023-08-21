import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App";
import reportWebVitals from "./reportWebVitals";
import { BrowserRouter as Router } from "react-router-dom";
import { Provider } from "react-redux";
import { configureStore } from "./store";
import { GoogleOAuthProvider } from "@react-oauth/google";
import { googleClientId } from "./services/common/const";
const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
  <GoogleOAuthProvider clientId={googleClientId}>
    <Provider store={configureStore({})}>
      {/* <React.StrictMode> */}{" "}
      {/**THis is commented because drag and drop are not working */}
      <Router>
        <App />
      </Router>
      {/* </React.StrictMode> */}
    </Provider>
  </GoogleOAuthProvider>
);
reportWebVitals();
