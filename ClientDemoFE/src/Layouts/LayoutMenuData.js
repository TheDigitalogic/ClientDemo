import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { getSessionUsingSessionStorage } from "../services/common/session";

const Navdata = () => {
  const navigate = useNavigate();
  //state data
  const [isUser, setIsUser] = useState(false);
  const [isOrganization, setIsOrganization] = useState(false);
  const [iscurrentState, setIscurrentState] = useState("User");
  const [userRole, setUserRole] = useState("Standered");
  function updateIconSidebar(e) {
    if (e && e.target && e.target.getAttribute("subitems")) {
      const ul = document.getElementById("two-column-menu");
      const iconItems = ul.querySelectorAll(".nav-icon.active");
      let activeIconItems = [...iconItems];
      activeIconItems.forEach((item) => {
        item.classList.remove("active");
        var id = item.getAttribute("subitems");
        if (document.getElementById(id))
          document.getElementById(id).classList.remove("show");
      });
    }
  }

  useEffect(() => {
    let promise = getSessionUsingSessionStorage();
    promise
      .then(function (value) {
        return value;
      })
      .then((value) => {
        setUserRole(value.role);
      });
  }, []);

  useEffect(() => {
    document.body.classList.remove("twocolumn-panel");
    if (iscurrentState !== "User") {
      setIsUser(false);
    }
    if (iscurrentState !== "Organization") {
      setIsOrganization(false);
    }
  }, [iscurrentState, isUser, isOrganization]);

  const menuItems =
    userRole?.toUpperCase() === "ADMIN"
      ? [
          {
            id: "user",
            label: "User",
            icon: "ri-user-line",
            link: "/user",
            stateVariables: isUser,
            click: function (e) {
              e.preventDefault();
              setIsUser(!isUser);
              setIscurrentState("User");
              updateIconSidebar(e);
            },
          },
          {
            id: "organization",
            label: "Organization",
            icon: "ri-dashboard-2-line",
            link: "/organization",
            stateVariables: isOrganization,
            click: function (e) {
              e.preventDefault();
              setIsOrganization(!isOrganization);
              setIscurrentState("Organization");
              updateIconSidebar(e);
            },
          },
        ]
      : [
          {
            id: "organization",
            label: "Organization",
            icon: "ri-dashboard-2-line",
            link: "/organization",
            stateVariables: isOrganization,
            click: function (e) {
              e.preventDefault();
              setIsOrganization(!isOrganization);
              setIscurrentState("Organization");
              updateIconSidebar(e);
            },
          },
        ];
  return <React.Fragment>{menuItems}</React.Fragment>;
};
export default Navdata;
