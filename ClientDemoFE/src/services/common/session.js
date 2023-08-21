/************************ SESSION UEING KEY - METHODS STARTS *****************************/

export function getSession(Key) {
  return readSessionData(Key);
}

export function setSession(Key, Value) {
  return storeSessionData(Key, Value);
}

async function readSessionData(Key) {
  var sessionValue = "";

  // sessionValue = ReactSession.get(Key);
  sessionValue = localStorage.getItem(Key);

  if (
    sessionValue === undefined ||
    sessionValue === null ||
    sessionValue === "" ||
    sessionValue === "undefined"
  ) {
    return {};
  } else {
    return sessionValue;
  }
}

async function storeSessionData(Key, Value) {
  // ReactSession.setStoreType("sessionStorage");
  // ReactSession.set(Key, Value);
  /**this is for set local storage */
  localStorage.setItem(Key, JSON.stringify(Value));
}

/**set local storage*/
export const setLocalStorageItem = (key, value) => {
  localStorage.setItem(key, value);
};
/**get local storage */
export const getLocalStorageItem = (key) => {
  localStorage.getItem(key);
};
/**remove local storage */
export const removeLocalStorageItem = (key) => {
  localStorage.removeItem(key);
};
/**get session data without using react session */
export const getSessionUsingSessionStorage = async () => {
  //fetching username from sesstion storage
  let sessionValue = "";
  //sessionValue = sessionStorage.getItem("__react_session__");
  const storedObjectString = localStorage.getItem("userDetails");
  sessionValue = JSON.parse(storedObjectString);
  // sessionValue = localStorage.getItem("userDetails");

  if (
    sessionValue === undefined ||
    sessionValue === null ||
    sessionValue === "" ||
    sessionValue === "undefined"
  ) {
    return {};
  } else {
    return sessionValue;
    // return JSON.parse(sessionValue)?.userDetails;
  }
};
/**This is check session login or not */
export const isLoggedIn = !!localStorage.getItem("userDetails");
/************************METHODS ENDS *****************************/
