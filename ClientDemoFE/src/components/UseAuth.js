// useAuth.js
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";

const UseAuth = () => {
  const navigate = useNavigate();
  const isLoggedIn = !!localStorage.getItem("userDetails"); // Replace with your actual authentication logic
  useEffect(() => {
    if (!isLoggedIn) {
      navigate("/login");
    }
  }, [isLoggedIn, navigate]);

  return isLoggedIn;
};

export default UseAuth;
