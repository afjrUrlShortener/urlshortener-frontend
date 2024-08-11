import { environment } from '../../environments/environment';

//Should NOT be used inside a component
//
//If it is needed, then the loggerService should be used instead
export const logger = {
  debug(message: any) {
    if (environment.PRODUCTION) return;

    console.debug(message);
  },
};
