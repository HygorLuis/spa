interface Env {
  apiUrl: string;
}

const _window = window as any;

export const environment = {
  production: true,
  apiUrl: _window.env.apiUrl
};
