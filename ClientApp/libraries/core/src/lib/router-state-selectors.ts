import * as fromRouter from '@ngrx/router-store';
import { createFeatureSelector, createSelector } from '@ngrx/store';
import { STATE_KEY } from './core.module';

const selectRouter = createFeatureSelector<any,
  fromRouter.RouterReducerState<any>>(STATE_KEY);

const {
  selectRouteParams
} = fromRouter.getSelectors(selectRouter);

export const selectRouteParameter = function(parameterName) {
  return createSelector(selectRouteParams, params => params[parameterName]);
};
