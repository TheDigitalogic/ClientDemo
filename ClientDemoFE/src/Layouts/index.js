import React, { useEffect } from "react";
//import Components
import Header from "./Header";
import Sidebar from "./Sidebar";
import Footer from "./Footer";
import { useState } from "react";
import { Outlet } from "react-router-dom";
//import actions
import {
  changeLayout,
  changeSidebarTheme,
  changeLayoutMode,
  changeLayoutWidth,
  changeLayoutPosition,
  changeTopbarTheme,
  changeLeftsidebarSizeType,
  changeLeftsidebarViewType,
  changeSidebarImageType,
} from "../store/actions";
//redux
import { useSelector, useDispatch } from "react-redux";
import { appName } from "../common/applicationName";
const Layout = () => {
  document.title = `${appName}-Home`;
  const [headerClass, setHeaderClass] = useState("");
  const dispatch = useDispatch();
  const {
    layoutType,
    leftSidebarType,
    layoutModeType,
    layoutWidthType,
    layoutPositionType,
    topbarThemeType,
    leftsidbarSizeType,
    leftSidebarViewType,
    leftSidebarImageType,
  } = useSelector((state) => ({
    layoutType: state.Layout.layoutType,
    leftSidebarType: state.Layout.leftSidebarType,
    layoutModeType: state.Layout.layoutModeType,
    layoutWidthType: state.Layout.layoutWidthType,
    layoutPositionType: state.Layout.layoutPositionType,
    topbarThemeType: state.Layout.topbarThemeType,
    leftsidbarSizeType: state.Layout.leftsidbarSizeType,
    leftSidebarViewType: state.Layout.leftSidebarViewType,
    leftSidebarImageType: state.Layout.leftSidebarImageType,
  }));
  /*
    layout settings
    */
  useEffect(() => {
    if (
      layoutType ||
      leftSidebarType ||
      layoutModeType ||
      layoutWidthType ||
      layoutPositionType ||
      topbarThemeType ||
      leftsidbarSizeType ||
      leftSidebarViewType ||
      leftSidebarImageType
    ) {
      dispatch(changeLeftsidebarViewType(leftSidebarViewType));
      dispatch(changeLeftsidebarSizeType(leftsidbarSizeType));
      dispatch(changeSidebarTheme(leftSidebarType));
      dispatch(changeLayoutMode(layoutModeType));
      dispatch(changeLayoutWidth(layoutWidthType));
      dispatch(changeLayoutPosition(layoutPositionType));
      dispatch(changeTopbarTheme(topbarThemeType));
      dispatch(changeLayout(layoutType));
      dispatch(changeSidebarImageType(leftSidebarImageType));
    }
  }, [
    layoutType,
    leftSidebarType,
    layoutModeType,
    layoutWidthType,
    layoutPositionType,
    topbarThemeType,
    leftsidbarSizeType,
    leftSidebarViewType,
    leftSidebarImageType,
    dispatch,
  ]);
  /*
    call dark/light mode
    */
  const onChangeLayoutMode = (value) => {
    if (changeLayoutMode) {
      dispatch(changeLayoutMode(value));
    }
  };

  // class add remove in header
  useEffect(() => {
    window.addEventListener("scroll", scrollNavigation, true);
  });

  function scrollNavigation() {
    var scrollup = document.documentElement.scrollTop;
    if (scrollup > 50) {
      setHeaderClass("topbar-shadow");
    } else {
      setHeaderClass("");
    }
  }
  return (
    <>
      <div id="layout-wrapper">
        <Header
          headerClass={headerClass}
          layoutModeType={layoutModeType}
          onChangeLayoutMode={onChangeLayoutMode}
        />
        <Sidebar layoutType={layoutType} />
        <div className="main-content">
          <Outlet />
          <Footer />
        </div>
      </div>
    </>
  );
};

export default Layout;
