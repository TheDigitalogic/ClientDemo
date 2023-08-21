import axios from "axios";
import {
  getSession,
  getSessionUsingSessionStorage,
  removeLocalStorageItem,
} from "./session";
var token = "";

export const get_old = (url, params) =>
  new Promise((resolve, reject) => {
    //Original working
    //var promise = getSession();

    //New Trial
    // var promise = getSession("userDetails");
    // const navigate = useNavigate();
    let promise = getSessionUsingSessionStorage();
    promise
      .then(function (value) {
        return value;
      })
      .then((value) => {
        token = value && (value.token ? value.token : "");

        //let options = {
        var headers = {
          Authorization: token ? `Bearer ${token}` : "",
          Accept: "application/json",
          "Content-Type": "application/json",
        };
        //};
        return fetch(url, { headers })
          .then((response) =>
            response.json().then((body) => ({
              ok: response.ok,
              status: response?.status,
              statusText: response?.statusText,
              data: body,
            }))
          )
          .then((responseJson) => {
            resolve(responseJson);

            if (!responseJson.ok) {
              if (responseJson.status === 400) {
                //400 = bad request
                if (responseJson.data && responseJson.data.message)
                  throw responseJson.data.message;
                else throw responseJson.statusText;
              } else throw responseJson.statusText;
            } else resolve(responseJson.data);
          })
          .catch((error) => {
            /**This is session storage clear add by sunil temprarory session storage clear when then 401 error */
            sessionStorage.clear();
            reject(error);
          });
      });
  });

export const post_old = (url, data = {}, fileToken = "") =>
  new Promise((resolve, reject) => {
    //Original working
    //var userDetails = getSession();

    //New Trial
    // var userDetails = getSession("userDetails");
    var userDetails = getSessionUsingSessionStorage();

    userDetails.then(function (value) {
      token = value && (value.token ? value.token : "");
    });
    let options = {
      headers: {
        Authorization: token ? `Bearer ${token}` : "",
        Accept: "application/json",
        "Content-Type": "application/json",
      },
    };
    let postOptions = { ...options.headers };
    if (fileToken.length > 0) {
      postOptions = {
        ...options.headers,
      };
    }
    return fetch(url, {
      method: "POST",
      headers: postOptions,
      body: JSON.stringify(data),
    })
      .then((response) =>
        response.json().then((body) => ({
          ok: response.ok,
          status: response?.status,
          statusText: response?.statusText,
          data: body,
        }))
      )
      .then((responseJson) => {
        if (!responseJson.ok) {
          if (responseJson.status === 400) {
            //400 = bad request
            if (responseJson.data && responseJson.data.message)
              throw responseJson.data.message;
            else throw responseJson.statusText;
          } else throw responseJson.statusText;
        } else resolve(responseJson.data);
      })
      .catch((error) => {
        reject(error);
      });
  });

/** This method does not have any specific content-type */
export const post_common = (url, data = {}, fileToken = "") =>
  new Promise((resolve, reject) => {
    var userDetails = getSessionUsingSessionStorage();
    userDetails.then(function (value) {
      token = value && (value.token ? value.token : "");
    });
    let options = {
      headers: {
        Authorization: token ? `Bearer ${token}` : "",
      },
    };
    let postOptions = { ...options.headers };
    if (fileToken.length > 0) {
      postOptions = {
        ...options.headers,
        //Authorization: fileToken ? `Bearer ${fileToken}` : "",
      };
    }
    return fetch(url, {
      method: "POST",
      headers: postOptions,
      body: data,
    })
      .then((response) =>
        response.json().then((body) => ({
          ok: response.ok,
          status: response?.status,
          statusText: response?.statusText,
          data: body,
        }))
      )
      .then((responseJson) => {
        if (!responseJson.ok) {
          if (responseJson.status === 400) {
            //400 = bad request
            if (responseJson.data && responseJson.data.message)
              throw responseJson.data.message;
            else throw responseJson.statusText;
          } else throw responseJson.statusText;
        } else resolve(responseJson.data);
      })
      .catch((error) => {
        reject(error);
      });
  });

/***This is post method from axios */
export const post = async (url, data = {}, fileToken = "") => {
  try {
    let promise = await getSessionUsingSessionStorage();
    let response = await axios.post(url, data, {
      headers: { Authorization: `Bearer ${promise.token}` },
    });
    return response.data;
  } catch (error) {
    if (error.response?.status === 401) {
      removeLocalStorageItem("userDetails");
      removeLocalStorageItem("currencySymbol");
      error.message = "Authorization error";
    } else if (error.response?.status === 400) {
      error.message = error.response.data.message;
    }
    return error;
  }
};

/**This is get method from axios */
export const get = async (url, params) => {
  try {
    let promise = await getSessionUsingSessionStorage();
    // UseAuth();
    const response = await axios.get(url, {
      headers: { Authorization: `Bearer ${promise.token}` },
    });
    return response;
  } catch (error) {
    if (error.response?.status === 401) {
      removeLocalStorageItem("userDetails");
      removeLocalStorageItem("currencySymbol");
      error.message = "Authorization error";
    } else if (error.response?.status === 400) {
      error.message = error.response.data.message;
    }
    return error;
  }
};
