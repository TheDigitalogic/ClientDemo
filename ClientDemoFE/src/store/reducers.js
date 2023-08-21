import { combineReducers } from "redux";
// Front
import Layout from "./layouts/reducer";
import destinationInformation from "./globalmasters/reducer";
const rootReducer = combineReducers({
  Layout,
  destinationInformation,
});

export default rootReducer;
