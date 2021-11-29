import * as React from 'react';

export default class Layout extends React.PureComponent<{}, { children?: React.ReactNode }> {
    public render() {
        return this.props.children
    }
}