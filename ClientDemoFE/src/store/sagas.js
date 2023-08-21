import { all, fork } from "redux-saga/effects";
//layout
import LayoutSaga from "./layouts/saga";
import destinationSaga from "./globalmasters/saga";
export default function* rootSaga() {
  yield all([
    //public
    fork(LayoutSaga),
    fork(destinationSaga),
  ]);
}
