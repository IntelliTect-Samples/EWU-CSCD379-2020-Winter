<template>
    <div class="modal is-active">
        <div class="modal-background"></div>
        <div class="modal-content">
            <div class="field">
                <label class="label has-text-white">Title</label>
                <div class="control">
                    <input id="titleInput" class="input" type="text" v-model="clonedGift.title" />
                </div>
            </div>
            <div class="field">
                <label class="label has-text-white">Description</label>
                <div class="control">
                    <input id="descriptionInput" class="input" type="text" v-model="clonedGift.description" />
                </div>
            </div>
            <div class="field">
                <label class="label has-text-white">Url</label>
                <div class="control">
                    <input id="urlInput" class="input" type="text" v-model="clonedGift.url" />
                </div>
            </div>
            <div class="field">
                <label class="label has-text-white">User</label>
                <div  class="select">
                    <select id="userInput" v-model="clonedGift.userId">
                        <option v-for="user in users" :value="user.id">
                            {{user.firstName}} {{user.lastName}}
                        </option>
                    </select>
                    <!--<input class="input" type="text" v-model.number="clonedGift.userId" />-->
                </div>
            </div>
            <div class="field is-grouped">
                <div class="control">
                    <button id="submit" class="button is-primary" @click.once="save">Submit</button>
                </div>
                <div class="control">
                    <a class="button" @click="cancel">Cancel</a>
                </div>
            </div>
        </div>
    </div>
</template>
<script lang="ts">
    import { Vue, Component, Prop, Emit } from 'vue-property-decorator'
    import { Gift, GiftClient, UserClient, User } from '../../secretsanta-client';
    @Component
    export default class GiftDetailsComponent extends Vue {
        @Prop()
        gift: Gift;
        clonedGift: Gift = <Gift>{};
        users: User[] = null;

        constructor() {
            super();
        }

        async mounted() {
            // get list of users for dropdown
            let userClient = new UserClient();
            this.users = await userClient.getAll();
            let tempGift = { ...this.gift };
            this.clonedGift = <Gift>tempGift;
        }

        @Emit('gift-saved')
        async save() {
            let giftClient = new GiftClient();
            if (this.clonedGift.id > 0) {
                await giftClient.put(this.clonedGift.id, this.clonedGift);
            }
            else {
                await giftClient.post(this.clonedGift);
            }
        }

        @Emit('gift-saved')
        cancel() {
        }
    }
</script>