import React, { useEffect } from "react";
import HomeRoutes from "./Routes";
/** import scss */
import "./assets/scss/themes.scss";
import { ToastContainer } from "react-toastify";
const App = () => {
  return (
    <>
      {/**this is toas container for notification */}
      <ToastContainer />
      {/**This is for routes */}
      <HomeRoutes />
    </>
  );
};

export default App;
