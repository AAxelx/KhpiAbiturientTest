import * as React from 'react';
import { connect } from 'react-redux';
import "../Site.css"
import { ApplicationState } from '../store';
import { actionCreators, Question } from '../store/SiteStore';
import Timer from './Timer';
import { RadioGroup, Radio } from '@adobe/react-spectrum'



type State = {
    email: string;
}

type StateProps = {
    trueEmail?: boolean
    question?: Question[]
}

type Props = typeof actionCreators & StateProps

class Home extends React.Component<Props,State>{

    constructor(props: Props) {
        super(props)
        this.state = {
            email: ""
        }
    }

    private handleChange = (event: any) => {
        this.setState({ email: event.target.value });
    }

    private handleSubmit = () => {
        this.props.sendEmail(this.state.email);
    }

    render() {
        return(
        <div className="over">
                <div className="main">
                    {this.props.trueEmail === undefined ? this.start() : (this.props.trueEmail ? this.props.question!.map(_ => this.checkbox(_)) : this.fail())}
            </div>
        </div>)
    }


    private start = () => {
        return(
            <>
                <h1>Добро пожаловать на тестирование!</h1>
                Чем больше правильных ответов вы дадите, тем больше ваши шансы на выигрыш в розыгрыше который пройдет 00.00.2022 в 00.00 в прямом эфире.
                Тест длится 5 минут и содержит 12 вопросов.
                Введите ваш email для связи и начинайте тест. Желаем удачи!
                <Timer />
                <form>
                    <label>
                        Введите Email:
                        <input type="text" value={this.state.email} onChange={this.handleChange} />
                    </label>
                    <input type="submit" value="Отправить" onClick={() => this.handleSubmit()} />
                </form>
            </>
        )
    }

    private fail = () => {
        return (
            <>
                <h1>Email уже был использован или неправильно введен</h1>
                Похоже этот адрес уже участвовал в опросе. Попробуйте другую електронную почту
            </>
        )
    }

    private checkbox = (q: Question) => {
        return (
            <div>
                <Timer />
                <RadioGroup label={q.question} defaultValue="" onChange={(value: string) => {
                    this.props.editAnswers(q.id, value);
                }}>
                    <Radio value={q.first}>{q.first}</Radio>
                    <Radio value={q.second}>{q.second}</Radio>
                    <Radio value={q.third}>{q.third}</Radio>
                </RadioGroup>
            </div>
        )
    }

    
}

const mapStateToProps = (state: ApplicationState) => {
    return {
        trueEmail: state.question.trueEmail,
    };
}

export default connect(mapStateToProps)(Home)
