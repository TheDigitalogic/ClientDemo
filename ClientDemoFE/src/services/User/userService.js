import { url } from "../common/const";
import { get, post, post_common } from "../common/https";

/**This service to Authenticate login User */
export const login = (data) => post(`${url}/api/auth/login`, data);
/**This service to Signup User */
export const signup = (data) => post(`${url}/api/auth/CreateUser`, data);
/**This service to forgot password */
export const forgotPassword = (data) =>
  post(`${url}/api/auth/ForgotPassword`, data);
/***This services to password chanes save */
export const resetPassword = (data) =>
  post(`${url}/api/auth/ResetPassword`, data);
/***This service for fresh token generate for forgot password */
export const changePassword = (data) =>
  post(`${url}/api/auth/ChangePassword`, data);
/**This service for save user company */
export const saveCompany = (data) =>
  post(`${url}/api/auth/SaveUserCompany`, data);
/**This service for get user and company details */
export const getUserAndCompany = (params) => {
  const getData = get(`${url}/api/auth/getUserCompany?` + params);
  return getData;
};
/**This service to Update User */
export const updateUser = (data) =>
  post(`${url}/api/auth/UpdateUserclient`, data);
/**This service for delete */
export const deleteUserService = (data) =>
  post(`${url}/api/auth/DeleteUser`, data);

/**This service for save user company */
export const updateCompany = (data) =>
  post(`${url}/api/auth/UpdateUserCompany`, data);
export const saveIamgeProfile = (data) =>
  post_common(`${url}/api/auth/SaveProfileImage`, data);
/**This services for login with google */
export const loginWithSocial = (data) =>
  post(`${url}/api/auth/googleLogin`, data);
/**This services for email verification */
export const userVerifyEmail = (data) =>
  post(`${url}/api/auth/VerifyUserEmail`, data);
/**This services for get users list */
export const getUserList = () => {
  const getData = get(`${url}/api/auth/List`);
  return getData;
};
