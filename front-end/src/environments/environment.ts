interface Env {
  apiUrl: string;
}

const _window = window as any;

export const environment = {
  production: false,
  apiUrl: _window.env.apiUrl
};
