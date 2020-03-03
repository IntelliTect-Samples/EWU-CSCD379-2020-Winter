<template>
    <div>
        <div class="field">
            <label class="label">First Name </label>
            <div class="control">
                <input class="input" type="text" v-model="clonedUser.firstName"/>
            </div>
        </div>
        <div class="field">
            <label class="label">Last Name</label>
            <div class="control">
                <input class="input" type="text" v-model="clonedUser.lastName"/>
            </div>
        </div>
        <div class="field is-grouped">
            <div class="control">
                <button id="submit" class="button" @click.once="saveUser">Submit</button>
            </div>
            <div class="control">
                <a class="button is-primary" @click="cancelEdit">Cancel</a>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    import {Vue, Component, Prop, Emit} from 'vue-property-decorator'
    import {User, UserClient} from '../../secretsanta-client'

    @Component
    export default class UserDetailsComponent extends Vue {
        @Prop()
        user: User;
        clonedUser: User = <User>{};

        constructor() {
            super();
        }

        mounted() {
            let tempUser = {...this.user};
            this.clonedUser = <User>tempUser;
        }

        @Emit("user-saved")
        async saveUser() {
            let userClient = new UserClient();
            if (this.clonedUser.id > 0) {
                await userClient.put(this.clonedUser.id, this.clonedUser);
            } else {
                await userClient.post(this.clonedUser);
            }

        }

        @Emit("user-saved")
        cancelEdit() {
        }
    }
</script>
