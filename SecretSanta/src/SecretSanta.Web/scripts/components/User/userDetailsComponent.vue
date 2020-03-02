<template>
    <div>
        <div class="field">
            <label class="label">First Name</label>
            <div class="control">
                <input class="input" type="text" v-model="user.firstName" />
            </div>
        </div>
        <div class="field">
            <label class="label">Last Name</label>
            <div class="control">
                <input class="input" type="text" v-model="user.lastName" />
            </div>
        </div>
        <div class="field is-grouped">
            <div class="control">
                <button id="submit" class="button is-primary" @click.once='saveUser'>Submit</button>
            </div>
            <div class="control">
                <a asp-action="Index" class="button is-light" @click='cancelEdit'>Cancel</a>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    import { Vue, Component, Prop, Emit } from 'vue-property-decorator';
    import { User, UserClient } from '../../secretsanta-client';
    @Component
    export default class UserDetailsComponent extends Vue {
        @Prop()
        user: User;
        constructor() {
            super();
        }

        @Emit('user-saved')
        async saveUser() {
            let userClient = new UserClient();
            if (this.user.id > 0) {
                await userClient.put(this.user.id, this.user);
            }
            else {
                await userClient.post(this.user);
            }
        }
        @Emit('user-saved')
        cancelEdit() {

        }
    }
</script>