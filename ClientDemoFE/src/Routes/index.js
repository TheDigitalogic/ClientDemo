import React from "react";
import { Routes, Route } from "react-router-dom";
import Login from "../pages/Login";
import Layout from "../Layouts";
import Signup from "../pages/Register";
import CreatePassword from "../pages/CreatePassword";
import Logout from "../pages/Logout";
import PickupDrop from "../pages/Masters/PickupDrop";
import RegistrationSuccess from "../pages/RegisteredSuccessfull";
import ForgotSuccess from "../pages/ForgotSuccess";
import ForgotPasswordScreen from "../pages/ForgotPassword";
import ResetPassword from "../pages/ResetPassword";
import ChangePasswordSuccess from "../pages/ChangePasswordSuccess";
import VerifyEmail from "../pages/VerifyEmail";
import User from "../pages/UserOrganization/User";
import Organization from "../pages/UserOrganization/Organization";
const AllRoutes = () => {
  return (
    <>
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route path="/signup" element={<Signup />} />
        <Route path="/forgotpassword" element={<ForgotPasswordScreen />} />
        <Route path="/createpassword" element={<CreatePassword />} />
        <Route path="/logout" element={<Logout />} />
        <Route path="/registrationsuccess" element={<RegistrationSuccess />} />
        <Route path="/forgotsuccess" element={<ForgotSuccess />} />
        <Route path="/verifyUserEmail" element={<VerifyEmail />} />
        <Route path="/resetpassword" element={<ResetPassword />} />
        <Route
          path="/passwordchangesuccess"
          element={<ChangePasswordSuccess />}
        />
        <Route path="/" element={<Layout />}>
          <Route path="/user" element={<User />} />
          <Route path="/organization" element={<Organization />} />
        </Route>
      </Routes>
    </>
  );
};

export default AllRoutes;
