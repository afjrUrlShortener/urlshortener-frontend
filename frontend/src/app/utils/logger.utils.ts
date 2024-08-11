import { environment } from '../../environments/environment';

export const logger = {
  debug(message: any) {
    if (environment.PRODUCTION) return;

    console.debug(message);
  },
};
