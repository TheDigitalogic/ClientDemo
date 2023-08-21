import React, { useEffect } from "react";
import PropTypes from "prop-types";
import { Link } from "react-router-dom";
import { Collapse } from "reactstrap";
// Import Data
import navdata from "../LayoutMenuData";
const VerticalLayout = () => {
  const navData = navdata().props.children;
  return (
    <React.Fragment>
      {/* menu Items */}
      {(navData || []).map((item, key) => {
        return (
          <React.Fragment key={key}>
            {/* Main Header */}
            {item["isHeader"] ? (
              <li className="menu-title">
                <span data-key="t-menu">{item.label}</span>
              </li>
            ) : item.subItems ? (
              <li className="nav-item">
                <Link
                  onClick={item.click}
                  className="nav-link menu-link"
                  to={item.link ? item.link : "/#"}
                  data-bs-toggle="collapse"
                >
                  <i className={item.icon}></i>
                  <span data-key="t-apps">{item.label}</span>
                  {item.badgeName ? (
                    <span
                      className={"badge badge-pill bg-" + item.badgeColor}
                      data-key="t-new"
                    >
                      {item.badgeName}
                    </span>
                  ) : null}
                </Link>
                <Collapse
                  className="menu-dropdown"
                  isOpen={item.stateVariables}
                  id="sidebarApps"
                >
                  <ul className="nav nav-sm flex-column test">
                    {/* subItms  */}
                    {item.subItems &&
                      (item.subItems || []).map((subItem, key) => (
                        <React.Fragment key={key}>
                          {!subItem.isChildItem ? (
                            <li className="nav-item">
                              <Link
                                to={subItem.link ? subItem.link : "/#"}
                                className="nav-link"
                              >
                                {subItem.label}
                                {subItem.badgeName ? (
                                  <span
                                    className={
                                      "badge badge-pill bg-" +
                                      subItem.badgeColor
                                    }
                                    data-key="t-new"
                                  >
                                    {subItem.badgeName}
                                  </span>
                                ) : null}
                              </Link>
                            </li>
                          ) : (
                            <li className="nav-item">
                              <Link
                                onClick={subItem.click}
                                className="nav-link"
                                to="/#"
                                data-bs-toggle="collapse"
                              >
                                {subItem.label}
                              </Link>
                              <Collapse
                                className="menu-dropdown"
                                isOpen={subItem.stateVariables}
                                id="sidebarEcommerce"
                              >
                                <ul className="nav nav-sm flex-column">
                                  {/* child subItms  */}
                                  {subItem.childItems &&
                                    (subItem.childItems || []).map(
                                      (childItem, key) => (
                                        <React.Fragment key={key}>
                                          {!childItem.childItems ? (
                                            <li className="nav-item">
                                              <Link
                                                to={
                                                  childItem.link
                                                    ? childItem.link
                                                    : "/#"
                                                }
                                                className="nav-link"
                                              >
                                                {childItem.label}
                                              </Link>
                                            </li>
                                          ) : (
                                            <li className="nav-item">
                                              <Link
                                                to="/#"
                                                className="nav-link"
                                                onClick={childItem.click}
                                                data-bs-toggle="collapse"
                                              >
                                                {childItem.label}
                                                <span
                                                  className="badge badge-pill bg-danger"
                                                  data-key="t-new"
                                                >
                                                  New
                                                </span>
                                              </Link>
                                              <Collapse
                                                className="menu-dropdown"
                                                isOpen={
                                                  childItem.stateVariables
                                                }
                                                id="sidebaremailTemplates"
                                              >
                                                <ul className="nav nav-sm flex-column">
                                                  {childItem.childItems.map(
                                                    (subChildItem, key) => (
                                                      <li
                                                        className="nav-item"
                                                        key={key}
                                                      >
                                                        <Link
                                                          to={subChildItem.link}
                                                          className="nav-link"
                                                          data-key="t-basic-action"
                                                        >
                                                          {subChildItem.label}
                                                        </Link>
                                                      </li>
                                                    )
                                                  )}
                                                </ul>
                                              </Collapse>
                                            </li>
                                          )}
                                        </React.Fragment>
                                      )
                                    )}
                                </ul>
                              </Collapse>
                            </li>
                          )}
                        </React.Fragment>
                      ))}
                  </ul>
                </Collapse>
              </li>
            ) : (
              <li className="nav-item">
                <Link
                  className="nav-link menu-link"
                  to={item.link ? item.link : "/#"}
                >
                  <i className={item.icon}></i> <span>{item.label}</span>
                  {item.badgeName ? (
                    <span
                      className={"badge badge-pill bg-" + item.badgeColor}
                      data-key="t-new"
                    >
                      {item.badgeName}
                    </span>
                  ) : null}
                </Link>
              </li>
            )}
          </React.Fragment>
        );
      })}
    </React.Fragment>
  );
};

VerticalLayout.propTypes = {
  location: PropTypes.object,
  t: PropTypes.any,
};

export default VerticalLayout;
