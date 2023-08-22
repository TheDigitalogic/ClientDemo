import React, { useEffect } from "react";
import HomeRoutes from "./Routes";
/** import scss */
import "./assets/scss/themes.scss";
import "react-toastify/dist/ReactToastify.css";
import { ToastContainer } from "react-toastify";
const App = () => {
  return (
    <>
      {/**this is toast container for notification */}
      <ToastContainer />
      {/**This is for routes */}
      <HomeRoutes />
    </>
  );
};

export default App;
