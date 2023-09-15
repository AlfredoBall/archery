import { League, useUpdateLeagueMutation } from '../../../gql/generated';

import { TextField, Button, Paper, Input } from '@mui/material';
import RestorePageIcon from '@mui/icons-material/RestorePage';
import Snackbar from '@mui/material/Snackbar';
import MuiAlert, { AlertProps } from '@mui/material/Alert';
import { useForm } from 'react-hook-form';
import { ErrorMessage } from '@hookform/error-message';
import React, { useState } from 'react';

interface FormInputs {
    name: string;
}

const Alert = React.forwardRef<HTMLDivElement, AlertProps>(function Alert(
    props,
    ref,
  ) {
    return <MuiAlert elevation={6} ref={ref} variant="filled" {...props} />;
});

const Item = ({ league } : { league: League | null }) => {

    const [isSuccessOpen, setIsSuccessOpen] = useState(false);

    const [isErrorOpen, setIsErrorOpen] = useState(false);

    const [isReadOnly, setIsReadOnly] = useState(true);

    const { register, handleSubmit, reset, control, setValue, formState: { isSubmitting, errors: formErrors } } = useForm<FormInputs>({
        criteriaMode: 'all',
        defaultValues: {
            name: league? league.name : ''
        }
    });

    const [sendRequest, response] = useUpdateLeagueMutation();

    const onSubmit = async (formData : any) => {
        await sendRequest({input: {
                id: league?.id,
                name: formData.name
        }})
        .then((result : any) => {
            if (result['error']) {
                setIsErrorOpen(true);

                reset();
            } else {
                reset(result.data.updateLeague.league);

                setIsSuccessOpen(true);
            }

            setIsReadOnly(true);
        })
    };

    return (
        <main>
            <form onSubmit={handleSubmit(onSubmit)}>
                <Input placeholder='Name'
                inputProps={{
                    readOnly: isReadOnly,
                    defaultValue: league?.name
                }}
                    {...register("name", {
                        required: "This input is required.",
                        minLength: {
                            value: 5,
                            message: "Min length is 5"
                        }})}/>
                <Button disabled={!isReadOnly} onClick={() => setIsReadOnly(false)}>Edit</Button>
                <Input disabled={isReadOnly} type="submit"/>
                <RestorePageIcon onClick={() => {reset(); setIsReadOnly(true)}}/>
                <ErrorMessage
                    errors={formErrors}
                    name="name"
                    render={({ messages }) => {
                        console.log("messages", messages);
                        return messages &&
                            Object.entries(messages).map(([type, message]) => (
                                <p key={type}>{message}</p>
                            ))
                        }
                    }
                />
            </form>
            <Snackbar open={isSuccessOpen} autoHideDuration={6000} onClose={() => setIsSuccessOpen(false)}>
                <Alert severity="success" sx={{ width: '100%' }}>
                    This is a success message!
                </Alert>
            </Snackbar>
            <Snackbar open={isErrorOpen} autoHideDuration={6000} onClose={() => setIsErrorOpen(false)}>
                <Alert severity="error" sx={{ width: '100%' }}>
                    This is a error message!
                </Alert>
            </Snackbar>
        </main>
    )
}

export default Item;